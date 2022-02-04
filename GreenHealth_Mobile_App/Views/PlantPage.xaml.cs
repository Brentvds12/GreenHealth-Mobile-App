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
    public partial class PlantPage : ContentPage
    {
        RestService restService;
        public PlantPage()
        {
            InitializeComponent();
            restService = new RestService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            PlantList.ItemsSource = await restService.GetPlants();
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            Plant plant = e.Item as Plant;
            Result result = await restService.GetResult(plant.Id);
            await Navigation.PushAsync(new PlantDetailPage(plant, result));
        }

        /*private async void tempNavButton_Clicked(object sender, EventArgs e)
        {
            Plant plant = new Plant(1);
            Result result = await restService.GetResult(plant.Id);
            await Navigation.PushAsync(new PlantDetailPage(plant, result));
        }*/
    }
}