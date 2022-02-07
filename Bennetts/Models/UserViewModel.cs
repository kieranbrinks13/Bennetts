using BennettsDataModels;
using Newtonsoft.Json;
using System.Text;

namespace Bennetts.Models
{
    public class UserViewModel : BaseViewModel
    {
        private readonly string ApiUrl = "https://localhost:7194";

        public UserDM NewUser { get; set; }
        public List<UserDM> Users { get; set; } = new List<UserDM>();

        internal async Task PopulateUserVM()
        {
            NewUser = new UserDM();

            var client = new HttpClient { BaseAddress = new Uri(ApiUrl) };
            var response = await client.GetAsync("users");
            var content = await response.Content.ReadAsStringAsync();

            Users = JsonConvert.DeserializeObject<List<UserDM>>(content);
        }

        internal async Task SaveUser()
        {
            var json = JsonConvert.SerializeObject(NewUser);

            var client = new HttpClient { BaseAddress = new Uri(ApiUrl) };
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("newuser", content);
            response.EnsureSuccessStatusCode();
        }

        internal async void DeleteUser(Guid userId)
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiUrl) };
            var response = await client.DeleteAsync($"deleteuser/{userId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
