using GreenHealth_Mobile_App.Models;
using GreenHealth_Mobile_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenHealth_Mobile_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantDetailPage : ContentPage
    {
        Plant plant;
        public PlantDetailPage(Plant plant)
        {
            InitializeComponent();
            plantImage.Source = new UriImageSource()
            {
                Uri = new Uri("" + plant.ImagePath)
            }
        }
    }
}