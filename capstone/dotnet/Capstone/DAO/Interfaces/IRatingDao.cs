using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
        public List<Rating> ListRatingByGameId(int gameId);

        public Rating GetRating(int gameId, int userId);

        public Rating AddRating(Rating rating);

        public Rating UpdateRating(Rating rating);
        public List<Rating> ListRatingsByUserId(int userId);
        public bool DeleteRating(int gameId,int userId);
    }
}
