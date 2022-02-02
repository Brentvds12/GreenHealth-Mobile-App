using GreenHealth_Mobile_App.Models;
using GreenHealth_Mobile_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GreenHealth_Mobile_App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Plant> Plants { get; set; }

        private RestService _restService { get; set; }

        public BaseViewModel()
        {
            _restService = new RestService();
            FillPlants();
        }

        public async Task FillPlants()
        {
            Plants = new ObservableCollection<Plant>(await _restService.GetPlants(1));
        }
    }
}
