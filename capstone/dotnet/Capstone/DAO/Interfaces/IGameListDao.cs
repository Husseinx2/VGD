using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IGameListDao
    {
        public List<GameList> ListGameListsByUserId(int userId);

        public GameList GetGameList(int listId);

        public GameList AddGameList(GameList gameList);

        public GameList UpdateGameList(GameList gameList);

        public bool DeleteGameList(int listId);

        public List<string> ListGames();

    }
}
