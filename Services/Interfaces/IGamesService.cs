using SteamMicroservice.Model.Games;
using SteamMicroservice.Model.Users;

namespace SteamMicroservice.Services.Interfaces
{
    public interface IGamesService
    {
        IAsyncEnumerable<Game> GetOwnedGames(string userId);

        IAsyncEnumerable<Game> UpdateGameDetails();

        IAsyncEnumerable<Game> GetCollection(string userId);
    }
}
