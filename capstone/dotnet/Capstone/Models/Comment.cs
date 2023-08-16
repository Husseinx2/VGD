using System;

namespace Capstone.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public int CommenterId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}
