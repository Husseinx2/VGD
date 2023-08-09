using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRatingDao
    {
        public List<Rating> ListRatingsByGameId(int gameId);

        public Rating GetRating(int gameId, int userId);

        public Rating AddRating(int gameId, int userId);

        public Rating UpdateRating(int gameId, int userId);

        public bool DeleteRating(int gameId);
    }
}
