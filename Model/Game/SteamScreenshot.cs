namespace SteamMicroservice.Model.Game
{
    public class SteamScreenshot
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public string Thumbnail { get; set; }
        public string Full {  get; set; }
        public SteamGame Game { get; set; }
    }
}