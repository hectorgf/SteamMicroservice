namespace SteamMicroservice.Model.Game
{
    public class SteamPrice
    { 
        public string? Currency {  get; set; }
        public long? Initial {  get; set; }
        public long? Final { get; set; }
        public long? Discount { get; set;}
        public decimal? FormatedInitial
        {
            get 
            {
                if (this.Initial != null)
                    return (this.Initial / 100);
                else
                    return null;
            }
        }
        public decimal? FormatedFinal
        {
            get
            {
                if (this.Initial != null)
                    return (this.Final / 100);
                else
                    return null;
            }
        }
    }
}