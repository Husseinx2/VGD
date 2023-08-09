using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
<<<<<<< HEAD
        public List<Rating> ListRatingByGameId(int gameId);

        public Rating GetRating(int gameId, int userId);

        public Rating AddRating(Rating rating);

        public Rating UpdateRating(Rating rating);
=======
        public List<Rating> ListRatingsByGameId(int gameId);
>>>>>>> fe087d160fb9ba93eff8e66019f65b296849c0e2
        public List<Rating> ListRatingsByUserId(int userId);
        public bool DeleteRating(int gameId,int userId);
    }
}
