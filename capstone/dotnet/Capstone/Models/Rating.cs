using System;

namespace Capstone.Models
{
    public class Rating
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int RatingValue { get; set; }
        public DateTime RatingDateTime { get; set; }
    }
}
