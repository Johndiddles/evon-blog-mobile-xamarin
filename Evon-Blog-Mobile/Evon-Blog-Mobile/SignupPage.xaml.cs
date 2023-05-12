using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvonBlogMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EvonBlogMobile
{	
	public partial class SignupPage : ContentPage
	{	
		 public SignupPage ()
		{
            InitializeComponent ();
            
		}

        async void SignUp_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string confirm_password = confirmPasswordEntry.Text;
            
            bool isUsernameEmpty = string.IsNullOrEmpty(username);
            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);
            bool isConfirmPasswordEmpty = string.IsNullOrEmpty(confirm_password);

            if (isUsernameEmpty || isEmailEmpty || isPasswordEmpty || isConfirmPasswordEmpty)
            {
                await DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            if (!String.Equals(password, confirm_password))
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
                return;
            }

            var content = await ApiServices.ServiceClientInstance.CreateUserAsync(username, email, password);
            if (!string.IsNullOrEmpty(content.token))
            {
                string user = Preferences.Get("username", "");
                Task task = DisplayAlert("Success", $"Signed up successfully, {user}", "Continue");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Failed", "Invalid Credentials", "Continue");
            }
        }

        async void Login_Label_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Evon_Blog_Mobile.MainPage());
        }
    }
}

