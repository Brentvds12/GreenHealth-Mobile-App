using GreenHealth_Mobile_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        public async Task LoginAsync(String email, String password)
        {
            var tempClient = new HttpClient();
            tempClient.BaseAddress = new Uri("https://greenhealth-api.azurewebsites.net/api/user/authenticate");

            string jsonData = @"{""email"" : """ + email + @""", ""password"" : """ + password + @"""}";
            Console.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await tempClient.PostAsync("", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                AuthResult jsonResult = JsonConvert.DeserializeObject<AuthResult>(result);
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonResult.token);
            }
            
        }
    }
}
