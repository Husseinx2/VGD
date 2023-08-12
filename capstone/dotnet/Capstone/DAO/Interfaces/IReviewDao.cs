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

<<<<<<< HEAD
        public List<Review> ListReviewsByReviewerId(int reviewerId);
        public List<Review> ListReviewsByReviewId(int reviewId);
=======
>>>>>>> 2300e8460721f9cb1d478323d05cfec5aac75120
        public bool DeleteReview(int reviewId);

        public bool DeleteReviewsByGameId(int gameId);

        public bool DeleteReviewsByReviewerId(int reviewerId);
    }
}
