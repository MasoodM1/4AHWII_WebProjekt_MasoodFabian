namespace _4AHWII_WebProjekt_MasoodFabian.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
