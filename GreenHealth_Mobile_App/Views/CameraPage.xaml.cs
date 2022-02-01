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
        public CameraPage()
        {
            InitializeComponent();
        }

        async void PickButton_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            var stream = await result.OpenReadAsync();

            if(stream != null)
            {
                resultImage.Source = ImageSource.FromStream(() => stream);

                RestService restService = new RestService();
                Console.WriteLine("RestService aangemaakt");

                Plant plant = new Plant(1);
                Console.WriteLine("new plant created: " + plant.Id);
                Plant newPlant = await restService.PostPlant(plant);
                Console.WriteLine("new plant posted: " + newPlant.Id);
                Plant resultPlant = await restService.PatchPlant(newPlant.Id, stream);
                Console.WriteLine("plant patched: " + resultPlant.Id);
                await Navigation.PushAsync(new PlantDetailPage(resultImage));
                Console.WriteLine("navigation attempted");

                /*ConfirmButton.IsVisible = true;*/
            }
        }

        async void MakeButton_Clicked(Object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            var stream = await result.OpenReadAsync();

            if (stream != null)
            {
                resultImage.Source = ImageSource.FromStream(() => stream);

                RestService restService = new RestService();
                Console.WriteLine("RestService aangemaakt");

                Plant plant = new Plant(1);
                Console.WriteLine("new plant created: " + plant.Id);
                Plant newPlant = await restService.PostPlant(plant);
                Console.WriteLine("new plant posted: " + newPlant.Id);
                Plant resultPlant = await restService.PatchPlant(newPlant.Id, stream);
                Console.WriteLine("plant patched: " + resultPlant.Id);
                await Navigation.PushAsync(new PlantDetailPage(resultImage));
                Console.WriteLine("navigation attempted");

                /*ConfirmButton.IsVisible = true;*/
            }
        }

        /*async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            RestService restService = new RestService();

            Plant plant = new Plant(1);
            Console.WriteLine("new plant created: " + plant.Id);
            Plant newPlant = await restService.PostPlant(plant);
            Console.WriteLine("new plant posted: " + newPlant.Id);
            Plant resultPlant = await restService.PatchPlant(newPlant.Id);
            Console.WriteLine("plant patched: " + resultPlant.Id);
            await Navigation.PushAsync(new PlantDetailPage(resultImage));
            Console.WriteLine("navigation attempted");
        }*/
    }
}