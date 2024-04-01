using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model.Game;
using SteamMicroservice.Model.User;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public class GameService : IGamesService
    {
        private IConfiguration _config;
        private string BASIC_URL;
        private string API_KEY;

        public GameService(IConfiguration config)
        {
            _config = config;
            BASIC_URL = _config["APIURLs:Players"];
            API_KEY = _config["APIKey"];
        }

        public async IAsyncEnumerable<OwnedGame> GetOwnedGames(string userId)
        {
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
                    foreach (var game in result.response.games)
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

        public async Task<SteamGameData> GetGameDetails(string gameId)
        {
            using (var client = new HttpClient())
            {
                var url = $"http://store.steampowered.com/api/appdetails?appids={gameId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(content);
                    var gameDataContent = jObject[gameId]["data"].ToString();
                    var gameData = JsonConvert.DeserializeObject<SteamGameData>(gameDataContent);
                    return gameData;
                }
                else
                {
                    throw new Exception($"Error al obtener los detalles del juego: {response.StatusCode}");
                }
            }
        }
    }
}
