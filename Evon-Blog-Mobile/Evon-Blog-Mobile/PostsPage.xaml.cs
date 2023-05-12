using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using EvonBlogMobile.Services;

namespace EvonBlogMobile
{
    public partial class PostsPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();
        private const string url = "https://evon-blog-be-django-production.up.railway.app/api/get-posts/";
        private ObservableCollection<Models.Posts> post;


        public PostsPage()
        {
            InitializeComponent();
        }

        async override protected void OnAppearing()
        {
            string responseContent = await _client.GetStringAsync(url);
            List<Models.Posts> mylist = JsonConvert.DeserializeObject<List<Models.Posts>>(responseContent);
            post = new ObservableCollection<Models.Posts>(mylist);
            ItemListView.ItemsSource = post;
            base.OnAppearing();
        }

        async void ItemListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var selected_post = e.Item as Models.Posts;
            await Navigation.PushAsync(new SinglePost(selected_post.id, selected_post.author_username, selected_post.author_id, selected_post.title, selected_post.body, selected_post.created_at));
            ((ListView)sender).SelectedItem = null;
        }
    }
}

