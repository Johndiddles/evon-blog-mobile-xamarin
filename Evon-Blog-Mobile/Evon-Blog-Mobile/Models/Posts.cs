using System;
namespace EvonBlogMobile.Models
{
    public class Posts
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string author_username { get; set; }
        public int author_id { get; set; }
        public DateTime created_at { get; }
    }

    public class NewPost
    {
        public string title { get; set; }
        public string body { get; set; }
    }

    public class NewPostReturnType
    {
        public string message { get; set; }
        public string errorMessage { get; set; }
    }

}

