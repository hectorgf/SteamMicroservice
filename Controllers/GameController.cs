using Microsoft.AspNetCore.Mvc;
using SteamMicroservice.Model.Game;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IGamesService _gameService;

        public GameController(ILogger<UserController> logger, IGamesService gamesService)
        {
            _logger = logger;
            _gameService = gamesService;
        }

        [HttpGet("GetOwnedGames")]
        public async IAsyncEnumerable<OwnedGame> GetOwnedGames(string userID)
        {
            var games = _gameService.GetOwnedGames(userID);
            await foreach (var game in games)
            {
                yield return game;
            }
        }
    }
}