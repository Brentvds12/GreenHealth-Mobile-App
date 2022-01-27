using GreenHealth_Mobile_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GreenHealth_Mobile_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            RestService restService = new RestService();

            if (isEmailEmpty || isPasswordEmpty)
            {
                //no login
            }
            else
            {
                await restService.LoginAsync(emailEntry.Text, passwordEntry.Text);
                await Navigation.PushAsync(new MenuPage());
            }
        }
    }
}
