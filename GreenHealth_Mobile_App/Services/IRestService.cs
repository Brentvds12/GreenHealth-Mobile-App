using GreenHealth_Mobile_App.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenHealth_Mobile_App.Services
{
    public interface IRestService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<List<Plant>> GetPlants(int userId);
        Task<Plant> GetPlant(int plantId);
        Task<Plant> PatchPlant(int id, Stream stream);
        Task<Plant> PostPlant(Plant plant);
    }
}