using GreenHealth_Mobile_App.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public interface IRestService
    {
        Task LoginAsync(string email, string password);
        Task<List<Plant>> GetPlants();
        Task<Plant> PatchPlant(int id, IFormFile image);
        Task<Plant> PostPlant(Plant plant);
    }
}