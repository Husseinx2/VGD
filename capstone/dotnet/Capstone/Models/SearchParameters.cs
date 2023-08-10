namespace Capstone.Models
{
    public class SearchParameters
    {
        public string Title { get; set; }
        public int Year { get; set; } = 0;
        public string ESRBRating { get; set; }
        public string GenreName { get; set; }
        public string PlatformName { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
    }
}
