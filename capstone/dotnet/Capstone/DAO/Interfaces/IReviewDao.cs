using Capstone.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDAO
    {
        public List<Review> ListReviewsByGameId(int gameId);

        public List<Review> ListReviewsByReviewerId(int reviewerId);

        public Review GetReview(int reviewId);

        public Review AddReview(Review review);

        public Review UpdateReview(Review review);

        public List<Review> ListReviewsByReviewId(int reviewId);

        public bool DeleteReview(int reviewId);

        public bool DeleteReviewsByGameId(int gameId);

        public bool DeleteReviewsByReviewerId(int reviewerId);
    }
}
