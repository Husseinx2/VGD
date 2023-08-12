using System;

namespace Capstone.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewContent { get; set; }
        public DateTime ReviewDateTime { get; set; }
    }
}
