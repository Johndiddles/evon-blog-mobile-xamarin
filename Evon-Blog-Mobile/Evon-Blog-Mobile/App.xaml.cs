using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Evon_Blog_Mobile
{
    public partial class App : Application
    {
        static readonly HttpClient client = new HttpClient();
        public App()
        {
            InitializeComponent();

            GetApi();
            MainPage = new NavigationPage(new MainPage());
        }

        private static async void GetApi()
        {
            var response = await client.GetStringAsync("https://evon-blog-be-django-production.up.railway.app/api/get-posts");
            var data = JsonConvert.DeserializeObject(response);
            Console.WriteLine(data);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

