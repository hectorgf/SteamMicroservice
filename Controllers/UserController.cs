using Microsoft.AspNetCore.Mvc;
using SteamMicroservice.Model.Users;
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

        [HttpGet("GetFriendList")]
        public async IAsyncEnumerable<Friend> GetFriendList(string userID)
        {
            var friends = _usersService.GetFriendList(userID);
            await foreach (var friend in friends)
            {
                yield return friend;
            }
        }
    }
}