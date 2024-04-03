namespace SteamMicroservice.Model.Game
{
    public class SteamCategory
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public string Description { get; set; }
        public IEnumerable<SteamGame> Games { get; set; }
    }
}