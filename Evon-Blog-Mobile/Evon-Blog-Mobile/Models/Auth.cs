using System;
namespace EvonBlogMobile.Models
{
    public class LoginDetails
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class SignupDetails
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public string username { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string token { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}

