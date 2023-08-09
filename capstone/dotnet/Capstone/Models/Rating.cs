using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Rating
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int RatingValue { get; set; }
        public DateTime RatingDatetime { get; set; }
    }
}
