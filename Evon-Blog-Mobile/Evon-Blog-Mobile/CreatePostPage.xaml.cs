using System;
using System.Collections.Generic;
using EvonBlogMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EvonBlogMobile
{
    public partial class CreatePostPage : ContentPage
    {
        public CreatePostPage()
        {
            InitializeComponent();
        }

        async void Publish_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string title = titleEntry.Text;
            string body = bodyEntry.Text;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Error", "Title cannot be empty", "OK");
                return;
            }

            if (string.IsNullOrEmpty(body))
            {
                await DisplayAlert("Error", "Body cannot be empty", "OK");
                return;
            }


            var content = await ApiServices.ServiceClientInstance.CreateNewPostAsync(title, body);

            if (String.Equals(content.message, "success"))
            {
                await DisplayAlert("Success", "Published post successfully", "Continue");
                await Navigation.PushAsync(new HomePage());
            }

            else
            {
                await DisplayAlert("Error", $"An error occured while publishing", "OK");
            }


        }
    }
}

