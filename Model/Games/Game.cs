using SteamMicroservice.Model.Users;
using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Games
{
    public class Game
    {
        public Guid Id { get; set; }
        public long SteamId { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public SteamGameType? Type { get; set; }
        public string? Name { get; set; }
        public int? RequiredAge { get; set; }
        public bool? IsFree { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? AboutGame { get; set; }
        public string? Languages { get; set; }
        public string? HeaderImage { get; set; }
        public string? CapsuleImage { get; set; }
        public string? CapsuleImageV5 { get; set; }
        public string? Website {  get; set; }
        public virtual ICollection<SteamRequirement> Requirements { get; set; }
        public virtual ICollection<SteamDeveloper> Developers { get; set; }
        public virtual ICollection<SteamPublisher> Publishers { get; set; }
        public SteamPrice? Price { get; set; }
        public bool? Windows {  get; set; }
        public bool? MacOS { get; set; }
        public bool? Linux { get; set; }
        public virtual ICollection<SteamCategory> Categories { get; set; }
        public virtual ICollection<SteamGenre> Genres { get; set; }
        public virtual ICollection<SteamScreenshot> Screenshots { get; set; }
        public long? Recomendations { get; set; }
        public SteamReleaseDate? ReleaseDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }
    }
}
