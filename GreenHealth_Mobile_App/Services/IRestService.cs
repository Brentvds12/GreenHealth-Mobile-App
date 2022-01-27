using System;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public interface IRestService
    {
        Task LoginAsync(string email, string password);
    }
}