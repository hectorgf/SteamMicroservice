using SteamMicroservice.Model.Games;
using System.Text.Json.Serialization;

namespace SteamMicroservice.Model.Users
{
    public class Player
    {
        public Guid Id { get; set; }
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int? commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string avatarhash { get; set; }
        public int lastlogoff { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
        public string? loccountrycode { get; set; }
        public string? locstatecode { get; set; }
        public int? loccityid { get; set; }

        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}