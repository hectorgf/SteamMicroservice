using SteamMicroservice.Model.Game;
using SteamMicroservice.Model.User;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IGamesService
    {
        IAsyncEnumerable<OwnedGame> GetOwnedGames(string userId);

        Task<SteamGameData> GetGameDetails(string gameId);
    }
}
