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
        public async IAsyncEnumerable<SteamGame> GetOwnedGames(string userID)
        {
            await foreach (var game in _gameService.GetOwnedGames(userID, false))
            {
                yield return game;
            }
        }

        [HttpGet("GetDetailedOwnedGames")]
        public async IAsyncEnumerable<SteamGame> GetDetailedOwnedGames(string userID)
        {
            await foreach (var game in _gameService.GetOwnedGames(userID, true))
            {
                yield return game;
            }
        }

        [HttpGet("GetGameDetails")]
        public async Task<SteamGame> GetGameDetails(int gameId)
        {
            return await _gameService.GetGameDetails(gameId);
        }
    }
}