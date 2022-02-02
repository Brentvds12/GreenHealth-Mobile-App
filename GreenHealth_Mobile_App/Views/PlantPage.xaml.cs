using GreenHealth_Mobile_App.Models;
using GreenHealth_Mobile_App.ViewModels;
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
        public PlantPage()
        {
            InitializeComponent();
            BindingContext = new BaseViewModel();
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new PlantDetailPage(e.Item as Plant));
        }
    }
}