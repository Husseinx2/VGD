using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
        public List<Rating> ListRatingsByGameId(int ratingId);

<<<<<<< HEAD
        public Rating GetRating(int ratingId, int userId);
=======
        public List<Rating> ListRatingsByUserId(int userId);

        public Rating GetRating(int gameId, int userId);
>>>>>>> c96b305c0eacf0ee23a1a4e4ab3a2da184e411d2

        public Rating AddRating(Rating rating);

        public Rating UpdateRating(int ratingId, int userId);

        public bool DeleteRating(int ratingId);
    }
}
