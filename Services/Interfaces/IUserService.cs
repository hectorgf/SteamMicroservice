using SteamMicroservice.Model.Game;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IUserService
    {
        IAsyncEnumerable<Player> GetPlayer(string userId);
    }
}
