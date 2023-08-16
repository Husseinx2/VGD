using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IGameListDao
    {
        public List<GameListEntry> GetGameList(int userId, string gameListType);

        public bool AddGameToList(GameListEntry entry);

        public bool DeleteGameFromList(GameListEntry entry);
    }
}
