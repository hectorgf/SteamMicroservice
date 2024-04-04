using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model;
using SteamMicroservice.Model.Configuration;
using SteamMicroservice.Model.Game;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public class GameService : IGamesService
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

        public async IAsyncEnumerable<OwnedGame> GetOwnedGames(string userId, bool withDetails)
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
        public async IAsyncEnumerable<SteamGame> GetGameDetails(IEnumerable<OwnedGame> games)
        {
            var tasks = games.Select(game => GetGameDetailsAsync(game));

            foreach (var task in tasks)
            {
                yield return await task;
            }
        }

        private async Task<SteamGame> GetGameDetailsAsync(OwnedGame game)
        {
            SteamGame detailedGame = await GetGameFromBD(game.appid);

            if (detailedGame != null)
                return detailedGame;

            using (var client = new HttpClient())
            {
                var url = $"http://store.steampowered.com/api/appdetails?appids={game.appid}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(content);
                    var gameDataContent = jObject[game.appid.ToString()]["data"].ToString();
                    var gameData = JsonConvert.DeserializeObject<SteamGameData>(gameDataContent);

                    detailedGame = await ConvertGame(gameData);
                    _context.Games.Add(detailedGame);
                    await _context.SaveChangesAsync();

                    return detailedGame;
                }
                else
                {
                    throw new Exception($"Error al obtener los detalles del juego: {response.StatusCode}");
                }
            }
        }

        private Task<SteamGame> GetGameFromBD(int gameId)
        {
            try
            {
                return _context.Games.Where(x => x.SteamId == gameId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<SteamGame> ConvertGame(SteamGameData game)
        {
            try
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
                    Price = new SteamPrice
                    {
                        Currency = game.price_overview.currency,
                        Initial = game.price_overview.initial,
                        Final = game.price_overview.final,
                        Discount = game.price_overview.discount_percent
                    },
                    Windows = game.platforms.windows,
                    MacOS = game.platforms.mac,
                    Linux = game.platforms.linux,
                    Categories = GetGameCategories(game.categories),
                    Genres = GetGameGenres(game.genres),
                    Screenshots = ConvertScreenshots(game.screenshots),
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
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                List<SteamDeveloper> steamDevelopers = new List<SteamDeveloper>();

                foreach (var developer in developers)
                {
                    SteamDeveloper dev = _context.Developers.Where(x => x.Name == developer).FirstOrDefault();

                    if (dev != null)
                        steamDevelopers.Add(dev);
                    else
                        steamDevelopers.Add(new SteamDeveloper
                        {
                            Name = developer
                        });
                }

                return steamDevelopers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<SteamPublisher> GetGamePublishers(string[] publishers)
        {
            try
            {
                List<SteamPublisher> steamPublisher = new List<SteamPublisher>();

                foreach (var publisher in publishers)
                {
                    SteamPublisher pub = _context.Publishers.Where(x => x.Name == publisher).FirstOrDefault();

                    if (pub != null)
                        steamPublisher.Add(pub);
                    else
                        steamPublisher.Add(new SteamPublisher
                        {
                            Name = publisher
                        });
                }

                return steamPublisher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<SteamScreenshot> ConvertScreenshots(Screenshot[] screenshots)
        {
            try
            {
                List<SteamScreenshot> steamCategory = new List<SteamScreenshot>();

                foreach (var screenshot in screenshots)
                {
                    steamCategory.Add(new SteamScreenshot
                    {
                        SteamId = screenshot.id,
                        Full = screenshot.path_full,
                        Thumbnail = screenshot.path_thumbnail
                    });
                }

                return steamCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<SteamGenre> GetGameGenres(Genre[] genres)
        {
            try
            {
                List<SteamGenre> steamCategory = new List<SteamGenre>();

                foreach (var genre in genres)
                {
                    SteamGenre cat = _context.Genres.Where(x => x.Description == genre.description).FirstOrDefault();

                    if (cat != null)
                        steamCategory.Add(cat);
                    else
                        steamCategory.Add(new SteamGenre
                        {
                            SteamId = genre.id,
                            Description = genre.description
                        });
                }

                return steamCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<SteamCategory> GetGameCategories(Category[] categories)
        {
            try
            {
                List<SteamCategory> steamCategory = new List<SteamCategory>();

                foreach (var category in categories)
                {
                    SteamCategory cat = _context.Categories.Where(x => x.Description == category.description).FirstOrDefault();

                    if (cat != null)
                        steamCategory.Add(cat);
                    else
                        steamCategory.Add(new SteamCategory
                        {
                            SteamId = category.id,
                            Description = category.description
                        });
                }

                return steamCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
