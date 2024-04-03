using SteamMicroservice.Model.Game;
using SteamMicroservice.Model.User;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IGamesService
    {
        IAsyncEnumerable<SteamGame> GetOwnedGames(string userId, bool withDetails);

        Task<SteamGame> GetGameDetails(int gameId);
    }
}
