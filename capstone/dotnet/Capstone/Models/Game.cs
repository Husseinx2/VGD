using System;

namespace Capstone.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ESRBRating { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
