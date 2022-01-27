using System;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public interface ILoginProvider
    {
        Task LoginAsync(string email, string password);
    }
}