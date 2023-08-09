using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Value { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
