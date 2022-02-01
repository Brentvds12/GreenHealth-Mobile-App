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
        public Stream savedStream;
        public CameraPage()
        {
            InitializeComponent();
        }

        async void PickButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            var stream = await result.OpenReadAsync();

            if(stream != null)
            {
                resultImage.Source = ImageSource.FromStream(() => stream);
                savedStream = stream;
                ConfirmButton.IsVisible = true;
            }
        }

        async void MakeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            var stream = await result.OpenReadAsync();

            if (stream != null)
            {
                resultImage.Source = ImageSource.FromStream(() => stream);
                savedStream = stream;
                ConfirmButton.IsVisible = true;
            }
        }

        async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            RestService restService = new RestService();

            Plant plant = new Plant(1);
            Console.WriteLine("new plant created");
            Plant newPlant = await restService.PostPlant(plant);
            Console.WriteLine("new plant posted");
            Console.WriteLine(savedStream);
            Plant resultPlant = await restService.PatchPlant(newPlant.Id, resultImage);
            Console.WriteLine("plant patched");
            await Navigation.PushAsync(new PlantDetailPage(resultImage));
            Console.WriteLine("navigation attempted");
        }
    }
}