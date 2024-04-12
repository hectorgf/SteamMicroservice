namespace SteamMicroservice.Model.Users
{
    public class Friend
    {
        public string SteamId { get; set; }
        public string Relationship { get; set; }
        public long FriendSince { get; set; }
        public DateTime FriendSinceDate
        {
            get
            {
                // Convertir el timestamp Unix a DateTimeOffset
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(FriendSince);

                // Convertir la fecha y hora a la zona horaria local
                return dateTimeOffset.LocalDateTime;
            }
        }
    }
}
