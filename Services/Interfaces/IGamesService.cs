using SteamMicroservice.Model.Game;
using SteamMicroservice.Model.User;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IGamesService
    {
        IAsyncEnumerable<OwnedGame> GetOwnedGames(string userId, bool withDetails);

        IAsyncEnumerable<SteamGame> GetGameDetails(IEnumerable<OwnedGame> games);
    }
}
