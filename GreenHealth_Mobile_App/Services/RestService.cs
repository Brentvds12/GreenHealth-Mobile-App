using GreenHealth_Mobile_App.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        String baseUrl = "https://greenhealth-api.azurewebsites.net/api/";

        public async Task LoginAsync(String email, String password)
        {
            var tempClient = new HttpClient();

            string jsonData = @"{""email"" : """ + email + @""", ""password"" : """ + password + @"""}";
            Console.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await tempClient.PostAsync(baseUrl + "user/authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                AuthResult jsonResult = JsonConvert.DeserializeObject<AuthResult>(result);
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonResult.token);
            }
        }

        public async Task<List<Plant>> GetPlants()
        {
            List<Plant> plants = new List<Plant>();

            using (_client)
            {
                HttpResponseMessage response = await _client.GetAsync(baseUrl + "plants");
                var json = await response.Content.ReadAsStringAsync();

                var plantsResponse = JsonConvert.DeserializeObject<PlantResponse>(json);

                plants = plantsResponse.plants as List<Plant>;
            }

            return plants;
        }

        public Task<Plant> PatchPlant(int id, IFormFile image)
        {
            throw new NotImplementedException();
        }

        public async Task<Plant> PostPlant(Plant plant)
        {
            string jsonData = JsonConvert.SerializeObject(plant);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (_client)
            {
                HttpResponseMessage response = await _client.PostAsync(baseUrl + "plants", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Plant plantResult = JsonConvert.DeserializeObject<Plant>(result);
                    return plantResult;
                }
                else return null;
            }           
        }
    }
}
