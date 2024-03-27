using Microsoft.AspNetCore.Mvc;
using SteamMicroservice.Model.Game;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _usersService;

        public UserController(ILogger<UserController> logger, IUserService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet("GetUserById")]
        public async IAsyncEnumerable<Player> GetUserById(string userID)
        {
            var players = _usersService.GetPlayer(userID);
            await foreach (var player in players)
            {
                yield return player;
            }
        }
    }
}