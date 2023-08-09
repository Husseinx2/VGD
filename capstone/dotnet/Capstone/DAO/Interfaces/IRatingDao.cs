using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
<<<<<<< HEAD
        public List<Rating> ListRatingByGameId(int gameId);

        public List<Rating> ListRatingByUserId(int userId);

        public Rating GetRating(int gameId, int userId);

        public Rating AddRating(Rating rating);

        public Rating UpdateRating(Rating rating);

        public bool DeleteRating(int gameId);
=======
        public List<Rating> ListRatingsByGameId(int gameId);
        public List<Rating> ListRatingsByUserId(int userId);
        public Rating GetRating(int gameId, int userId);
        public Rating AddRating(Rating rating);
        public Rating UpdateRating(Rating rating);
        public bool DeleteRating(int gameId,int userId);
>>>>>>> b565d3b217c961030d21cc63656bc6a32ecb8387
    }
}
