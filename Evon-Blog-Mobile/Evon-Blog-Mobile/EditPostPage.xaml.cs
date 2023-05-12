using System;
using System.Collections.Generic;
using EvonBlogMobile.Services;
using Xamarin.Forms;

namespace EvonBlogMobile
{
    public partial class EditPostPage : ContentPage
    {
        public EditPostPage(int id, string title, string body)
        {
            InitializeComponent();
            titleEntry.Text = title;
            bodyEntry.Text = body;
            Application.Current.Properties["id"] = id;
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

            var id = int.Parse(Application.Current.Properties["id"].ToString());
            var content = await ApiServices.ServiceClientInstance.EditPostAsync(id, title, body);

            if (String.Equals(content.message, "Updated post successfully"))
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

