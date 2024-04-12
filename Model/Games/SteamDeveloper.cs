using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Games
{
    public class SteamDeveloper
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Game> Games {  get; set; } 
    }
}