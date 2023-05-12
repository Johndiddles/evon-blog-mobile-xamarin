using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EvonBlogMobile;
using EvonBlogMobile.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Evon_Blog_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void loginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            bool isUsernameEmpty = string.IsNullOrEmpty(username);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isUsernameEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Ooops", "Username or password cannot be empty", "OK");
                return;
            }

            var content = await ApiServices.ServiceClientInstance.AuthenticateUserAsync(username, password);
            if (!string.IsNullOrEmpty(content.token))
            {
                string user = Preferences.Get("username", "");
                Task task = DisplayAlert("Success", $"Logged in successfully {user}", "Continue");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Failed", "Invalid Credentials", "Continue");
            }
        }

        async void Signup_Label_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}

