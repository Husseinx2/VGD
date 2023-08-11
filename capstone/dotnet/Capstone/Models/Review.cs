using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int ReviewerId { get; set; }
        public List<Comment> Comments { get; set; }
        public string ReviewContent { get; set; }
        public DateTime ReviewDateTime { get; set; }
    }
}
