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
        RestService _restService;
        public PlantDetailPage(Plant plant)
        {
            InitializeComponent();
            _restService = new RestService();
            this.plant = plant;
            plantImage.Source = new UriImageSource()
            {
                Uri = new Uri("https://storagemainfotosplanten.blob.core.windows.net/greenhealth/" + plant.ImagePath)
            };
            FetchResult();
        }

        public async Task FetchResult()
        {
            result = await _restService.GetResult(plant.Id);
            resultText.Text = "Week " + result.GrowthStage;
            resultConfidence.Text = result.Accuracy + "%";
        }
    }
}