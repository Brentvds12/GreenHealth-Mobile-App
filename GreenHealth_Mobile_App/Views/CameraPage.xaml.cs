using System;
using System.Collections.Generic;
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
                ConfirmButton.IsVisible = true;
            }
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}