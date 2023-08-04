using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IGameDao
    {
        public List<Game> ListGames();

        public Game GetGame(int gameId);

        public Game AddGame(Game game);

        public Game UpdateGame(Game game);

        public bool DeleteGame(int gameId);
    }
}
