using GreenHealth_Mobile_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GreenHealth_Mobile_App.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Plant> plants;

        public ObservableCollection<Plant> Plants
        {
            get { return plants; }
            set
            {
                plants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("plants"));
            }
        }

        public TestViewModel()
        {

        }
    }
}
