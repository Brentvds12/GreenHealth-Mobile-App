using GreenHealth_Mobile_App.Models;
using GreenHealth_Mobile_App.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenHealth_Mobile_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        private readonly RestService _restService;
        public CameraPage()
        {
            InitializeComponent();
            _restService = new RestService() ;
        }

        async void PickButton_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result == null)
            {
                return;
            }

            var stream = await result.OpenReadAsync();
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            resultImage.Source = ImageSource.FromStream(() => memoryStream);

            Plant plant = new Plant(1);
            Plant newPlant = await _restService.PostPlant(plant);
            Plant resultPlant = await _restService.PatchPlant(newPlant.Id, stream);

            await Navigation.PushAsync(new PlantDetailPage(resultPlant));
        }

        async void MakeButton_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result == null)
            {
                return;
            }

            var stream = await result.OpenReadAsync();
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            resultImage.Source = ImageSource.FromStream(() => memoryStream);

            Plant plant = new Plant(1);
            Plant newPlant = await _restService.PostPlant(plant);
            Plant resultPlant = await _restService.PatchPlant(newPlant.Id, stream);

            await Navigation.PushAsync(new PlantDetailPage(resultPlant));
        }
    }
}