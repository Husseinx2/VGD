using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ESRBRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Platforms { get; set; }
        public List<string> Developers { get; set; }
        public List<string> Publishers { get; set; }
    }
}
