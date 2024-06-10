namespace _4AHWII_WebProjekt_MasoodFabian.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePosted { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public BlogPost()
        {
        }

        public BlogPost(int id, string title, string content, string imageUrl, DateTime datePosted, User user)
        {
            Id = id;
            Title = title;
            Content = content;
            ImageUrl = imageUrl;
            DatePosted = datePosted;
            User = user;
        }
    }
}
