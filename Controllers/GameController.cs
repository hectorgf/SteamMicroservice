using Microsoft.AspNetCore.Mvc;
using SteamMicroservice.Model.Games;
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
        public async IAsyncEnumerable<Game> GetOwnedGames(string userId)
        {
            await foreach (var game in _gameService.GetOwnedGames(userId))
            {
                yield return game;
            }
        }

        [HttpGet("GetCollection")]
        public async IAsyncEnumerable<Game> GetCollection(string userId)
        {
            await foreach(var game in _gameService.GetCollection(userId))
            {
                yield return game;
            }
        }

        [HttpPut("GetGameDetails")]
        public async IAsyncEnumerable<Game> UpdateGameDetails()
        {
            await foreach (var game in _gameService.UpdateGameDetails())
            {
                yield return game;
            }
        }
    }
}