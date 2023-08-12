using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface ICommentDao
    {
        public List<Comment> ListCommentsByReviewId(int reviewId);

        public List<Comment> ListCommentsByCommenterId(int commenterId);

        public Comment GetComment(int commentId);

        public Comment AddComment(Comment comment);

        public Comment UpdateComment(Comment comment);

        public bool DeleteComment(int commentId);

        public bool DeleteCommentsByReviewId(int reviewId);

        public bool DeleteCommentsByCommenterId(int commenterId);
    }
}

