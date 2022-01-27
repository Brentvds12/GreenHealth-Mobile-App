using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public class LoginProvider : ILoginProvider
    {
        public async Task LoginAsync(String email, String password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://greenhealth-api.azurewebsites.net/api/user/authenticate");

            string jsonData = @"{""email"" : """ + email + @""", ""password"" : """ + password + @"""}";
            Console.WriteLine(jsonData);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();













            /*try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

            }
            catch (Exception er)
            {
                var lb = er.ToString();
                var js = "xs";
            }*/
        }
    }
}
