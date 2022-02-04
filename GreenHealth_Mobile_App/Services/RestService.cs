using GreenHealth_Mobile_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenHealth_Mobile_App.Services
{
    public class RestService : IRestService
    {
        // Basis Url voor de API.
        private const string baseUrl = "https://greenhealthapi.azurewebsites.net/api/";

        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        private readonly HttpClient _httpClient;

        JsonSerializerOptions serializerOptions;

        public List<Plant> plantList { get; set; }

        public RestService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        // Functie om de gebruiker in te loggen.
        public async Task<bool> LoginAsync(String email, String password)
        {
            string jsonData = @"{""email"" : """ + email + @""", ""password"" : """ + password + @"""}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(baseUrl + "user/authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                AuthResult jsonResult = JsonConvert.DeserializeObject<AuthResult>(result);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonResult.token);
                Application.Current.Properties["AppToken"] = jsonResult.token;
                return true;
            }
            else return false;
        }

        // Functie om alle planten op te vragen.
        public async Task<List<Plant>> GetPlants()
        {
            plantList = new List<Plant>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "Plants");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            try
            {
                var plantsResponse = System.Text.Json.JsonSerializer.Deserialize<List<Plant>>(json, serializerOptions);
                plantList = plantsResponse;
                return plantList;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        // Functie om een specifieke plant op te vragen
        public async Task<Plant> GetPlant(int plantId)
        {
            Plant plant;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "Plants/" + plantId);
            var json = await response.Content.ReadAsStringAsync();

            plant = JsonConvert.DeserializeObject<Plant>(json);
            return plant;
        }

        // Functie om een afbeelding aan een plant toe te voegen.
        public async Task<Plant> PatchPlant(int id, Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = PatchMethod,
                RequestUri = new Uri(baseUrl + "plants/" + id + "/image"),
                Content = new MultipartFormDataContent("Upload----" + DateTimeOffset.Now.ToString("O"))
                {
                    { streamContent, "image", Guid.NewGuid() + ".jpg" }
                }
            });

            var result = await response.Content.ReadAsStringAsync();

            Plant plantResult = JsonConvert.DeserializeObject<Plant>(result);
            return plantResult;
        }

        // Functie om een nieuwe plant aan te maken.
        public async Task<Plant> PostPlant(Plant plant)
        {
            string jsonData = JsonConvert.SerializeObject(plant);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            HttpResponseMessage response = await _httpClient.PostAsync(baseUrl + "plants", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                Plant plantResult = JsonConvert.DeserializeObject<Plant>(result);
                return plantResult;
            }

            return null;
        }

        //Functie om een analyseresultaat op te halen adhv een plantID
        public async Task<Result> GetResult(int plantId)
        {
            Result result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["AppToken"].ToString());

            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + "Plants/" + plantId + "/result");
            var json = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Result>(json);
            return result;
        }

        //Functie om een afbeelding tijdelijk op te slaan in de app
        public String SavePicture(string name, Stream data, string location = "temp")
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            documentsPath = Path.Combine(documentsPath, "Orders", location);
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, name);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
            return "temp/" + name;
        }
    }
}
