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
        Result result;
        public PlantDetailPage(Plant plant, Result result)
        {
            InitializeComponent();
            this.plant = plant;
            this.result = result;
            plantImage.Source = new UriImageSource()
            {
                Uri = new Uri("https://storagemainfotosplanten.blob.core.windows.net/greenhealth/" + plant.ImagePath)
            };
            
            resultText.Text = result.Species;
            resultConfidence.Text = "Week " + result.GrowthStage + " met " + Math.Round((decimal)result.Accuracy, 2) + "% zekerheid";          
        }
    }
}