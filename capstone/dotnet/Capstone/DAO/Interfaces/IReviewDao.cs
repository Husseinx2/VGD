using Capstone.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDAO
    {
        public List<Review> ListReviewsByGameId(int gameId);

        public Review GetReview(int reviewId);

        public Review AddReview(Review review);

        public Review UpdateReview(Review review);

        public List<Review> ListReviewsByReviewerId(int reviewerId);
        public bool DeleteReview(int reviewId);
    }
}
