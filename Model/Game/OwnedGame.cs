namespace SteamMicroservice.Model.Game
{
    public class OwnedGame
    {
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public int playtime_deck_forever { get; set; }
        public int rtime_last_played { get; set; }
        public int playtime_disconnected { get; set; }
        public int playtime_2weeks { get; set; }
    }

}
