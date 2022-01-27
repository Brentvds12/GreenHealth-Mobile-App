using GreenHealth_Mobile_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.Services
{
    public class ApiConnection : IApiConnection
    {
        public Task<ObservableCollection<Plant>> getplant()
        {
            throw new NotImplementedException();
        }

        public Task<Plant> patchPlant()
        {
            throw new NotImplementedException();
        }

        public Task<Plant> putPlant()
        {
            throw new NotImplementedException();
        }
    }
}
