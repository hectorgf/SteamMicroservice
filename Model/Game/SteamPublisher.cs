using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Game
{
    public class SteamPublisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<SteamGame> Games { get; set; }
    }
}