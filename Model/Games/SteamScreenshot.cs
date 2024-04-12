using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Games
{
    public class SteamScreenshot
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public string Thumbnail { get; set; }
        public string Full {  get; set; }

        [JsonIgnore]
        public virtual Game Game { get; set; }
    }
}