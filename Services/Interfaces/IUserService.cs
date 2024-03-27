using SteamMicroservice.Model.User;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IUserService
    {
        IAsyncEnumerable<Player> GetPlayer(string userId);

        IAsyncEnumerable<Friend> GetFriendList(string userId);
    }
}
