using GreenHealth_Mobile_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public interface IApiConnection
    {
        Task<ObservableCollection<Plant>> getplant();
        Task<Plant> patchPlant();
        Task<Plant> putPlant();
    }
}
