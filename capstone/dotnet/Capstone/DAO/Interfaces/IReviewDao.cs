using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDao
    {
        public List<Review> ListReviewsByGameId(int gameId);

        public List<Review> ListReviewsByReviewerId(int reviewerId);

        public Review GetReview(int reviewId);

        public Review AddReview(Review review);

        public Review UpdateReview(Review review);

        public bool DeleteReview(int reviewId);

        public bool DeleteReviewsByGameId(int gameId);

        public bool DeleteReviewsByReviewerId(int reviewerId);
    }
}
