using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model;
using SteamMicroservice.Model.Game;
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

        public async IAsyncEnumerable<SteamGame> GetOwnedGames(string userId, bool withDetails)
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
                        yield return await GetDetailedGame(game);
                    }
                }
                else
                {
                    // Si la solicitud no fue exitosa, muestra el código de estado
                    Console.WriteLine("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
                }
            }
        }

        public async Task<SteamGame> GetGameDetails(int gameId)
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
                    return await ConvertGame(gameData);
                }
                else
                {
                    throw new Exception($"Error al obtener los detalles del juego: {response.StatusCode}");
                }
            }
        }

        private async Task<SteamGame> GetDetailedGame(OwnedGame game)
        {
            throw new NotImplementedException();
        }

        private async Task<SteamGame> ConvertGame(SteamGameData game)
        {
            return new SteamGame
            {
                SteamId = game.steam_appid,
                Type = Enum.Parse<SteamGameType>(game.type),
                Name = game.name,
                RequiredAge = game.required_age,
                IsFree = game.is_free,
                Description = game.detailed_description,
                AboutGame = game.about_the_game,
                ShortDescription = game.short_description,
                Languages = game.supported_languages,
                HeaderImage = game.header_image,
                CapsuleImage = game.capsule_image,
                CapsuleImageV5 = game.capsule_imagev5,
                Website = game.website,
                Requirements = GetGameRequirements(game),
                Developers = GetGameDevelopers(game.developers),
                Publishers = GetGamePublishers(game.publishers),
                Windows = game.platforms.windows,
                MacOS = game.platforms.mac,
                Linux = game.platforms.linux,
                Categories = GetGameCategories(game.categories),
                Genres = GetGameGenres(game.genres),
                Screenshots = GetGameScreenshots(game.screenshots),
                Recomendations = game.recommendations.total,
                ReleaseDate = new SteamReleaseDate
                {
                    ComingSoon = game.release_date.coming_soon,
                    Date = new DateTime(Convert.ToInt32(game.release_date.date.Split(',')[1].Trim()),
                                        ConvertMonth(game.release_date.date.Split(',')[0].Split(' ')[1]),
                                        Convert.ToInt32(game.release_date.date.Split(',')[0].Split(' ')[0]))
                }
            };
        }

        private List<SteamRequirement> GetGameRequirements(SteamGameData game)
        {
            var requirements = new List<SteamRequirement>();

            if (game.pc_requirements != null)
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.PC,
                    Minimum = game.pc_requirements.minimum,
                    //Recomended = game.pc_requirements.recomended
                });

            if (game.mac_requirements != null)
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.MacOS,
                    Minimum = game.mac_requirements.minimum,
                    //Recomended = game.pc_requirements.recomended
                });

            if (game.linux_requirements != null)
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.Linux,
                    Minimum = game.linux_requirements.minimum,
                    //Recomended = game.pc_requirements.recomended
                });

            return requirements;
        }

        private IEnumerable<SteamDeveloper> GetGameDevelopers(string[] developers)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SteamPublisher> GetGamePublishers(string[] publishers)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SteamScreenshot> GetGameScreenshots(Screenshot[] screenshots)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SteamGenre> GetGameGenres(Genre[] genres)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SteamCategory> GetGameCategories(Category[] categories)
        {
            throw new NotImplementedException();
        }

        private int ConvertMonth(string month)
        {
            switch (month.ToLower())
            {
                case "jan": return 0;
                case "feb": return 1;
                case "mar": return 2;
                case "apr": return 3;
                case "may": return 4;
                case "jun": return 5;
                case "jul": return 6;
                case "aug": return 7;
                case "sep": return 8;
                case "oct": return 9;
                case "nov": return 10;
                case "dic": return 11;
                default: return -1;
            }
        }
    }
}
