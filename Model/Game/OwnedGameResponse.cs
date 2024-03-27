namespace SteamMicroservice.Model.Game
{
    public class OwnedGameResponse
    {
        public int game_count { get; set; }
        public OwnedGame[] games { get; set; }
    }
}
