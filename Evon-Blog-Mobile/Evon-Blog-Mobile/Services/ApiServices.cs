using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace EvonBlogMobile.Services
{
    public class ApiServices
    {
        private static ApiServices _ServiceClientInstance;
        private String BaseUrl = "https://evon-blog-be-django-production.up.railway.app";

        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                {
                    _ServiceClientInstance = new ApiServices();
                    return _ServiceClientInstance;
                }

                return _ServiceClientInstance;
            }
        }

        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient client;


        public ApiServices()
        {
            client = new HttpClient();
            //client.BaseAddress = new Uri("https://evon-blog-be-django-production.up.railway.app/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<EvonBlogMobile.Models.LoginResponse> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                Models.LoginDetails loginDetails = new Models.LoginDetails()
                {
                    username = username,
                    password = password
                };

                var serializeItem = JsonConvert.SerializeObject(loginDetails);
                StringContent body = new StringContent(serializeItem, Encoding.UTF8, "application/json");
               
                var response = await client.PostAsync($"{BaseUrl}/api/login", body);

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Models.LoginResponse>(json);
                    Preferences.Set("token", jsoncontent.token);
                    Preferences.Set("user_id", jsoncontent.id);
                    Preferences.Set("username", jsoncontent.username);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var failedUser = new Models.LoginResponse();
                return failedUser;
            }
        }

        public async Task<EvonBlogMobile.Models.LoginResponse> CreateUserAsync(string username, string email, string password)
        {
            try
            {
                Models.SignupDetails signupDetails = new Models.SignupDetails()
                {
                    username = username,
                    email = email,
                    password = password
                };

                var serializeItem = JsonConvert.SerializeObject(signupDetails);
                StringContent body = new StringContent(serializeItem, Encoding.UTF8, "application/json");
              

                var response = await client.PostAsync($"{BaseUrl}/api/create-user", body);

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Models.LoginResponse>(json);
                    Preferences.Set("token", jsoncontent.token);
                    Preferences.Set("user_id", jsoncontent.id);
                    Preferences.Set("username", jsoncontent.username);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var failedUser = new Models.LoginResponse();
                return failedUser;
            }
        }


        public async Task<Models.NewPostReturnType> CreateNewPostAsync(string title, string body)
        {
            try
            {
                Models.NewPost PostData = new Models.NewPost()
                {
                    title = title,
                    body = body,
                };

                var serializeItem = JsonConvert.SerializeObject(PostData);
                StringContent formData = new StringContent(serializeItem, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", ""));
                var response = await client.PostAsync($"{BaseUrl}/api/create-post", formData);

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Models.NewPostReturnType>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Models.NewPostReturnType error = new Models.NewPostReturnType();
                error.errorMessage = "Unkown error";

                return error;
            }
        }

        public async Task<Models.NewPostReturnType> EditPostAsync(int id, string title, string body)
        {
            try
            {
                Models.NewPost PostData = new Models.NewPost()
                {
                    title = title,
                    body = body,
                };

                var serializeItem = JsonConvert.SerializeObject(PostData);
                StringContent formData = new StringContent(serializeItem, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", ""));
                var response = await client.PutAsync($"{BaseUrl}/api/edit-post/{id}", formData);

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Models.NewPostReturnType>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Models.NewPostReturnType error = new Models.NewPostReturnType();
                error.errorMessage = "Unkown error";

                return error;
            }
        }

        public async Task<Models.NewPostReturnType> DeletePostAsync(int id)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", ""));
                var response = await client.DeleteAsync($"{BaseUrl}/api/delete-post/{id}");

                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<Models.NewPostReturnType>(json);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Models.NewPostReturnType error = new Models.NewPostReturnType();
                error.errorMessage = "Unkown error";

                return error;
            }
        }

    }
}

