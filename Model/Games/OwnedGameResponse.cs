namespace SteamMicroservice.Model.Games
{
    public class OwnedGameResponse
    {
        public int game_count { get; set; }
        public OwnedGame[] games { get; set; }
    }
}
