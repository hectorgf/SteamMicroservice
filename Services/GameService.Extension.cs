using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamMicroservice.Model;
using SteamMicroservice.Model.Configuration;
using SteamMicroservice.Model.Games;
using SteamMicroservice.Model.Users;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public partial class GameService : IGamesService
    {
        private async IAsyncEnumerable<Game> CreateOrUpdateGames(ICollection<OwnedGame> games, string userId)
        {
            Player? currentPlayer = _context.Players.Include(x => x.Games)
                .Where(x => x.steamid.Equals(userId)).FirstOrDefault();

            if (currentPlayer == null)
                throw new Exception("No se ha encontrado el jugador con el SteamId: " + userId + ".");

            foreach(var game in games)
            {
                var newGame = new Game
                {
                    SteamId = game.appid,
                    IsUpdated = false
                };

                if (!_context.Games.Any(x => x.SteamId == game.appid))
                    _context.Games.Add(newGame);
                else
                    newGame = _context.Games.Where(x => x.SteamId == game.appid).First();

                if (!currentPlayer.Games.Any(x => x.SteamId == game.appid))
                    currentPlayer.Games.Add(newGame);

                yield return newGame;
            }

            await _context.SaveChangesAsync();
        }

        private Task<Game> GetGameFromBD(long gameId)
        {
            try
            {
                return _context.Games.Where(x => x.SteamId == gameId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
