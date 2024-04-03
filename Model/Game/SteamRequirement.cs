namespace SteamMicroservice.Model.Game
{
    public class SteamRequirement
    {
        public Guid Id { get; set; }
        public RequirementType Type { get; set; }
        public string Minimum { get; set; }
        public string? Recomended {  get; set; }
        public SteamGame Game { get; set; }
    }
}