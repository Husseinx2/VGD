using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDAO
    {
        public List<Review> ListReviewsByGameId(int gameId);

        public Review GetReview(int gameId, int reviewerId);

        public Review AddReview(Review review);

        public Review UpdateReview(Review review);

        public List<Review> ListReviewsByReviewerId(int reviewerId);
        public bool DeleteReview(int gameId, int reviewerId);
        public bool DeleteReviewsByGameId(int gameId);
        public bool DeleteReviewsByReviewerId(int reviewerId);
    }
}
