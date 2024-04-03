namespace SteamMicroservice.Model.Game
{
    public class SteamGenre
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public string Description { get; set; }
        public IEnumerable<SteamGame> Games { get; set; }
    }
}