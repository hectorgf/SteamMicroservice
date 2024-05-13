using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model.Configuration;
using SteamMicroservice.Model.Games;
using SteamMicroservice.Model.Users;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public partial class GameService : IGamesService
    {
        private IConfiguration _config;
        private readonly SteamDbContext _context;
        private string BASIC_URL;
        private string API_KEY;

        public GameService(IConfiguration config, SteamDbContext context)
        {
            _config = config;
            _context = context;
            BASIC_URL = _config["APIURLs:Players"];
            API_KEY = _config["APIKey"];
        }

        public async IAsyncEnumerable<Game> GetOwnedGames(string userId)
        {
            if (!_context.Players.Where(x => x.steamid == userId).Any())
                throw new Exception("No existe el jugador especificado.");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    // Define la URL de la API
                    string url = BASIC_URL + "/GetOwnedGames/v0001/?";
                    url += "key=" + API_KEY + "&";
                    url += "steamid=" + userId;
                    url += "&format=json";

                    // Realiza la solicitud GET a la API
                    response = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    // Captura y muestra cualquier excepción que ocurra
                    Console.WriteLine("Error: " + ex.Message);
                }

                // Verifica si la solicitud fue exitosa (código de estado 200)
                if (response != null && response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena
                    string json = await response.Content.ReadAsStringAsync();
                    OwnedGameRoot result = JsonConvert.DeserializeObject<OwnedGameRoot>(json);
                    var completeGames = CreateOrUpdateGames(result.response.games, userId);

                    await foreach (var game in completeGames)
                    {
                        yield return game;
                    }
                }
                else
                {
                    // Si la solicitud no fue exitosa, muestra el código de estado
                    Console.WriteLine("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
                }
            }
        }

        public async IAsyncEnumerable<Game> UpdateGameDetails()
        {
            var games = await _context.Games.Where(x => x.IsUpdated == false).ToListAsync();

            foreach (var game in games)
            {
                using (var client = new HttpClient())
                {
                    var url = $"http://store.steampowered.com/api/appdetails?appids={game.SteamId}";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jObject = JObject.Parse(content);
                            if ((bool)jObject[game.SteamId.ToString()]["success"])
                            {
                                var gameDataContent = jObject[game.SteamId.ToString()]["data"].ToString();
                                var gameData = JsonConvert.DeserializeObject<SteamGameData>(gameDataContent);                                

                                UpdateGame(game, gameData);
                                _context.Games.Update(game);
                                await _context.SaveChangesAsync();
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        yield return game;
                    }
                    else
                    {
                        throw new Exception($"Error al obtener los detalles del juego: {response.StatusCode}");
                    }
                }
            }
        }

        public async IAsyncEnumerable<Game> GetCollection(string userId)
        {
            Player player = _context.Players.Where(x => x.steamid == userId).Include(x => x.Games).FirstOrDefault();

            if (player == null)
                throw new Exception("No existe el jugador especificado.");

            APILimiter apiLimiter = new APILimiter(2);

            foreach (var game in player.Games)
            {
                if (game.IsUpdated)
                    yield return game;
                else
                {
                    await apiLimiter.WaitBeforeRequest();
                    yield return UpdateGame(game).Result;
                }
            }
        }

        private async Task<Game> UpdateGame(Game game)
        {
            using (var client = new HttpClient())
            {
                var url = $"http://store.steampowered.com/api/appdetails?appids={game.SteamId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(content);
                        if ((bool)jObject[game.SteamId.ToString()]["success"])
                        {
                            var gameDataContent = jObject[game.SteamId.ToString()]["data"].ToString();
                            var gameData = JsonConvert.DeserializeObject<SteamGameData>(gameDataContent);

                            UpdateGame(game, gameData);
                            _context.Games.Update(game);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    return game;
                }
                else
                {
                    throw new Exception($"Error al obtener los detalles del juego: {response.StatusCode}");
                }
            }
        }
    }

    public class APILimiter
    {
        private DateTime lastRequestTime;
        private int requestsCount;
        private readonly int maxRequestsPerSecond;
        private readonly object lockObject = new object();

        public APILimiter(int maxRequestsPerSecond)
        {
            this.maxRequestsPerSecond = maxRequestsPerSecond;
            this.lastRequestTime = DateTime.MinValue;
            this.requestsCount = 0;
        }

        public async Task WaitBeforeRequest()
        {
            lock (lockObject)
            {
                var now = DateTime.Now;
                var elapsed = now - lastRequestTime;
                var interval = TimeSpan.FromSeconds(1.0 / maxRequestsPerSecond);

                if (elapsed < interval)
                {
                    var delay = interval - elapsed;
                    Thread.Sleep(delay);
                }

                lastRequestTime = DateTime.Now;
                requestsCount++;
            }
        }
    }

}
