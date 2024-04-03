namespace SteamMicroservice.Model.Game
{
    public class SteamPrice
    { 
        public string Currency {  get; set; }
        public long? Initial {  get; set; }
        public long? Final { get; set; }
        public long? Discount { get; set;}
        public string? FormatedInitial
        {
            get 
            {
                if (this.Initial != null)
                    return (this.Initial / 100).ToString();
                else
                    return null;
            }
        }
        public string? FormatedFinal
        {
            get
            {
                if (this.Initial != null)
                    return (this.Final / 100).ToString();
                else
                    return null;
            }
        }
    }
}