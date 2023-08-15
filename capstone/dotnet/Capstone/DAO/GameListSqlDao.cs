using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class GameListSqlDao : IGameListDao
    {
        private readonly string connectionString = "";

        private readonly string sqlListGames = "SELECT game_id FROM game " +
            "WHERE game_id = @game_id;";

        public GameListSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public GameList AddGameList(GameList gameList)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteGameList(int listId)
        {
            throw new System.NotImplementedException();
        }

        public GameList GetGameList(int listId)
        {
            throw new System.NotImplementedException();
        }

        public List<GameList> ListGameListsByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public GameList UpdateGameList(GameList gameList)
        {
            throw new System.NotImplementedException();
        }

        //public List<string> ListGames ()
        //{
        //    List<string> games = new List<string>();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(sqlListGames, conn))
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        games.Add(Convert.ToString(reader["game_id"]));

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        return new List<string>();
        //    }

        //    return games;
        //}

        //public bool AddGameJunction(GameList gameList)
        //{
        //    try
        //    {
        //        foreach (string genreName in gameList.Games)
        //        {
        //            int genreId = GetGameIdByName(gameName);

        //            if (genreId == 0)
        //            {
        //                AddGame(genreName);
        //                genreId = GetGameIdByName(gameName);
        //            }

        //            AddGameList(gamegameList.listId, gameId);
        //        }
        //        catch (SqlException)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        private GameList MapRowToGameList(SqlDataReader reader)
        {

            GameList gameList = new GameList();

            gameList.ListId = Convert.ToInt32(reader["list_id"]);
            gameList.UserId = Convert.ToInt32(reader["user_id"]);
            gameList.ListTitle = Convert.ToString(reader["list_title"]);
            gameList.ListType = Convert.ToString(reader["list_type"]);
            gameList.IsDefault = Convert.ToBoolean(reader["is_default"]);

            //gameList.Games = ListGames (gameList.ListId);

            return gameList;
        }

    }
}
