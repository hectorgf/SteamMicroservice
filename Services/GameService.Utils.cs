using SteamMicroservice.Model;
using SteamMicroservice.Model.Games;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public partial class GameService : IGamesService
    {
        private Game UpdateGame(Game game, SteamGameData gameData)
        {
            try
            {
                game.Type = Enum.Parse<SteamGameType>(gameData.type);
                game.Name = gameData.name;
                game.RequiredAge = gameData.required_age;
                game.IsFree = gameData.is_free;
                game.Description = gameData.detailed_description;
                game.AboutGame = gameData.about_the_game;
                game.ShortDescription = gameData.short_description;
                game.Languages = gameData.supported_languages;
                game.HeaderImage = gameData.header_image;
                game.CapsuleImage = gameData.capsule_image;
                game.CapsuleImageV5 = gameData.capsule_imagev5;
                game.Website = gameData.website;
                game.Requirements = GetGameRequirements(gameData);
                if (gameData.developers != null)
                    game.Developers = GetGameDevelopers(gameData.developers);
                game.Publishers = GetGamePublishers(gameData.publishers);
                if (gameData.price_overview != null)
                    game.Price = new SteamPrice
                    {
                        Currency = gameData.price_overview.currency,
                        Initial = gameData.price_overview.initial,
                        Final = gameData.price_overview.final,
                        Discount = gameData.price_overview.discount_percent
                    };
                game.Windows = gameData.platforms.windows;
                game.MacOS = gameData.platforms.mac;
                game.Linux = gameData.platforms.linux;
                if (gameData.categories != null)
                    game.Categories = GetGameCategories(gameData.categories);
                game.Genres = GetGameGenres(gameData.genres);
                game.Screenshots = ConvertScreenshots(gameData.screenshots);
                game.Recomendations = gameData.recommendations?.total;
                game.ReleaseDate = new SteamReleaseDate
                {
                    ComingSoon = gameData.release_date.coming_soon,
                    Date = new DateTime(Convert.ToInt32(gameData.release_date.date.Split(',')[1].Trim()),
                                        ConvertMonth(gameData.release_date.date.Split(',')[0].Split(' ')[1]),
                                        Convert.ToInt32(gameData.release_date.date.Split(',')[0].Split(' ')[0]))
                };
                game.IsUpdated = true;
                game.LastUpdateDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return game;
        }

        private List<SteamRequirement> GetGameRequirements(SteamGameData game)
        {
            var requirements = _context.Requirements.Where(x => x.Game.SteamId == game.steam_appid).ToList();

            if (game.pc_requirements != null 
                && (!string.IsNullOrEmpty(game.pc_requirements.recommended) || !string.IsNullOrEmpty(game.pc_requirements.minimum))
                && (requirements == null || !requirements.Any(x => x.Type == RequirementType.PC)))
            {
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.PC,
                    Minimum = game.pc_requirements.minimum,
                    Recomended = game.pc_requirements.recommended
                });
            }

            if (game.mac_requirements != null 
                && (!string.IsNullOrEmpty(game.mac_requirements.recommended) || !string.IsNullOrEmpty(game.mac_requirements.minimum))
                && (requirements == null || !requirements.Any(x => x.Type == RequirementType.MacOS)))
            {
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.MacOS,
                    Minimum = game.pc_requirements.minimum,
                    Recomended = game.pc_requirements.recommended
                });
            }

            if (game.linux_requirements != null
                && (!string.IsNullOrEmpty(game.linux_requirements.recommended) || !string.IsNullOrEmpty(game.linux_requirements.minimum))
                && (requirements == null || !requirements.Any(x => x.Type == RequirementType.Linux)))
            {
                requirements.Add(new SteamRequirement
                {
                    Type = RequirementType.Linux,
                    Minimum = game.pc_requirements.minimum,
                    Recomended = game.pc_requirements.recommended
                });
            }

            return requirements;
        }

        private ICollection<SteamDeveloper> GetGameDevelopers(string[] developers)
        {
            try
            {
                List<SteamDeveloper> steamDevelopers = new List<SteamDeveloper>();

                foreach (var developer in developers)
                {
                    SteamDeveloper dev = _context.Developers.Where(x => x.Name == developer).FirstOrDefault();

                    if (dev == null)
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

        private ICollection<SteamPublisher> GetGamePublishers(string[] publishers)
        {
            try
            {
                List<SteamPublisher> steamPublisher = new List<SteamPublisher>();

                foreach (var publisher in publishers)
                {
                    SteamPublisher pub = _context.Publishers.Where(x => x.Name == publisher).FirstOrDefault();

                    if (pub == null)
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

        private ICollection<SteamScreenshot> ConvertScreenshots(Screenshot[] screenshots)
        {
            try
            {
                List<SteamScreenshot> steamScreenshot = new List<SteamScreenshot>();

                foreach (var screenshot in screenshots)
                {
                    steamScreenshot.Add(new SteamScreenshot
                    {
                        SteamId = screenshot.id,
                        Full = screenshot.path_full,
                        Thumbnail = screenshot.path_thumbnail
                    });
                }

                return steamScreenshot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ICollection<SteamGenre> GetGameGenres(Genre[] genres)
        {
            try
            {
                List<SteamGenre> steamGenre = new List<SteamGenre>();

                foreach (var genre in genres)
                {
                    SteamGenre cat = _context.Genres.Where(x => x.Description == genre.description).FirstOrDefault();

                    if (cat == null)
                        steamGenre.Add(new SteamGenre
                        {
                            SteamId = genre.id,
                            Description = genre.description
                        });
                }

                return steamGenre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ICollection<SteamCategory> GetGameCategories(Category[] categories)
        {
            try
            {
                List<SteamCategory> steamCategory = new List<SteamCategory>();

                foreach (var category in categories)
                {
                    SteamCategory cat = _context.Categories.Where(x => x.Description == category.description).FirstOrDefault();

                    if (cat == null)
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
                case "jan": return 1;
                case "feb": return 2;
                case "mar": return 3;
                case "apr": return 4;
                case "may": return 5;
                case "jun": return 6;
                case "jul": return 7;
                case "aug": return 8;
                case "sep": return 9;
                case "oct": return 10;
                case "nov": return 11;
                case "dic": return 12;
                default: return -1;
            }
        }
    }
}
