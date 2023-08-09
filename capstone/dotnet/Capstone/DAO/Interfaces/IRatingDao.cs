using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
        public List<Rating> ListRatingsByGameId(int ratingId);

        public Rating GetRating(int ratingId, int userId);

        public Rating AddRating(Rating rating);

        public Rating UpdateRating(int ratingId, int userId);

        public bool DeleteRating(int ratingId);
    }
}
