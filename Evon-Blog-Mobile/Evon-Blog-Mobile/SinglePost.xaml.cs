using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using EvonBlogMobile.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace EvonBlogMobile
{
    public partial class SinglePost : ContentPage
    {


        public SinglePost(int id, string author_username, int author_id, string title, string body, DateTime created_at)
        {
            InitializeComponent();
            postTitleLabel.Text = title;
            postBodyLabel.Text = body;
            postAuthorLabel.Text = $"By: {author_username}";
            postTimeLabel.Text = $"Created: {created_at.ToString()}";

            Application.Current.Properties["id"] = id;
            Application.Current.Properties["title"] = title;
            Application.Current.Properties["body"] = body;

            int user_id = Preferences.Get("user_id", 0);
            if (user_id != author_id)
            {
                EditButton.IsVisible = false;
                DeleteButton.IsVisible = false;
            }
        }

        async void EditPostButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int id = int.Parse(Application.Current.Properties["id"].ToString());
            string title = Application.Current.Properties["title"].ToString();
            string body = Application.Current.Properties["body"].ToString();

            await Navigation.PushAsync(new EditPostPage(id, title, body));
        }

        async void DeletePostButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var should_delete = await DisplayAlert("Confirm", "Are you sure you want to delete ", "Delete", "Cancel");
            if (should_delete)
            {
                var id = int.Parse(Application.Current.Properties["id"].ToString());
                var content = await ApiServices.ServiceClientInstance.DeletePostAsync(id);

                if (String.Equals(content.message, "Deleted post successfully"))
                {
                    await DisplayAlert("Success", "Deleted post successfully", "Continue");
                    await Navigation.PushAsync(new HomePage());
                }

                else
                {
                    await DisplayAlert("Error", $"An error occured while deleting '{Application.Current.Properties["title"].ToString()}'", "OK");
                }
            }

        }
    }
}

