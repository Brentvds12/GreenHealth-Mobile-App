using GreenHealth_Mobile_App.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenHealth_Mobile_App.Services
{
    public class RestService : IRestService
    {
        // Basis Url voor de API.
        String baseUrl = "https://greenhealthapi.azurewebsites.net/api/";

        // Functie om de gebruiker in te loggen.
        public async Task<bool> LoginAsync(String email, String password)
        {
            var client = new HttpClient();

            string jsonData = @"{""email"" : """ + email + @""", ""password"" : """ + password + @"""}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseUrl + "user/authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                AuthResult jsonResult = JsonConvert.DeserializeObject<AuthResult>(result);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonResult.token);
                Application.Current.Properties["AppToken"] = jsonResult.token;
                return true;
            }
            else return false;
        }

        // Functie om alle planten op te vragen.
        public async Task<List<Plant>> GetPlants(int userId)
        {
            List<Plant> plants = new List<Plant>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Users/" + userId + "/plants");
                var json = await response.Content.ReadAsStringAsync();

                var plantsResponse = JsonConvert.DeserializeObject<PlantResponse>(json);

                plants = plantsResponse.plants as List<Plant>;
            }

            return plants;
        }

        // Functie om een specifieke plant op te vragen
        public async Task<Plant> GetPlant(int plantId)
        {
            Plant plant;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + "Plants/" + plantId);
                var json = await response.Content.ReadAsStringAsync();

                plant = JsonConvert.DeserializeObject<Plant>(json);
            }
            return plant;
        }

        // Functie om een afbeelding aan een plant toe te voegen.
        public async Task<Plant> PatchPlant(int id, Image image)
        {
            using (var formContent = new MultipartFormDataContent())
            {
                byte[] bitmapData;
                var stream = new MemoryStream();
                //image.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                bitmapData = stream.ToArray();
                var fileContent = new ByteArrayContent(bitmapData);

                formContent.Headers.ContentType.MediaType = "multipart/form-data";
                formContent.Add(new StreamContent(stream));

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

                using (client)
                {
                    try
                    {
                        var method = new HttpMethod("PATCH");

                        var request = new HttpRequestMessage(method, baseUrl + id + "/image")
                        {
                            Content = new StringContent(
                                            JsonConvert.SerializeObject(formContent),
                                            Encoding.UTF8, "application/json")
                        };

                        var response = await client.SendAsync(request);
                        var result = await response.Content.ReadAsStringAsync();

                        Plant plantResult = JsonConvert.DeserializeObject<Plant>(result);
                        return plantResult;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        // Functie om een nieuwe plant aan te maken.
        public async Task<Plant> PostPlant(Plant plant)
        {
            string jsonData = JsonConvert.SerializeObject(plant);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());


            using (client)
            {
                HttpResponseMessage response = await client.PostAsync(baseUrl + "plants", content);

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
