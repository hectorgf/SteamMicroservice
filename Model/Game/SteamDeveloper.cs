namespace SteamMicroservice.Model.Game
{
    public class SteamDeveloper
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SteamGame> Games {  get; set; } 
    }
}