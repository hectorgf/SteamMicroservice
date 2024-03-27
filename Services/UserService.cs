using Newtonsoft.Json;
using SteamMicroservice.Model.User;
using SteamMicroservice.Services.Interfaces;

namespace SteamMicroservice.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _config;
        private string BASIC_URL;
        private string API_KEY;

        public UserService(IConfiguration config)
        {
            _config = config;
            BASIC_URL = _config["APIURLs:Users"];
            API_KEY = _config["APIKey"];
        }

        public async IAsyncEnumerable<Player> GetPlayer(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    // Define la URL de la API
                    string url = BASIC_URL + "/GetPlayerSummaries/v0002/?";
                    url += "key=" + API_KEY + "&";
                    url += "steamids=" + userId;

                    // Realiza la solicitud GET a la API
                    response = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    // Captura y muestra cualquier excepción que ocurra
                    Console.WriteLine("Error: " + ex.Message);
                }

                // Verifica si la solicitud fue exitosa (código de estado 200)
                if (response != null && response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena
                    string json = await response.Content.ReadAsStringAsync();
                    UserRoot result = JsonConvert.DeserializeObject<UserRoot>(json);
                    foreach (var player in result.response.players)
                    {
                        yield return player;
                    }
                }
                else
                {
                    // Si la solicitud no fue exitosa, muestra el código de estado
                    Console.WriteLine("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
                }
            }
        }

        public async IAsyncEnumerable<Friend> GetFriendList(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    // Define la URL de la API
                    string url = BASIC_URL + "/GetFriendList/v0001/?";
                    url += "key=" + API_KEY + "&";
                    url += "steamid=" + userId;
                    url += "&relationship=friend";

                    // Realiza la solicitud GET a la API
                    response = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    // Captura y muestra cualquier excepción que ocurra
                    Console.WriteLine("Error: " + ex.Message);
                }

                // Verifica si la solicitud fue exitosa (código de estado 200)
                if (response != null && response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena
                    string json = await response.Content.ReadAsStringAsync();
                    FriendsListRoot result = JsonConvert.DeserializeObject<FriendsListRoot>(json);
                    foreach (var friend in result.FriendsList.Friends)
                    {
                        yield return friend;
                    }
                }
                else
                {
                    // Si la solicitud no fue exitosa, muestra el código de estado
                    Console.WriteLine("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
                }
            }
        }
    }
}
