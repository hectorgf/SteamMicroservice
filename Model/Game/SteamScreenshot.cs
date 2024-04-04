using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Game
{
    public class SteamScreenshot
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public string Thumbnail { get; set; }
        public string Full {  get; set; }

        [JsonIgnore]
        public virtual SteamGame Game { get; set; }
    }
}