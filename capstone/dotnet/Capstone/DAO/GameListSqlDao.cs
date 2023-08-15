using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class GameListSqlDao : IGameListDao
    {
        private readonly string connectionString = "";

        private readonly string sqlAddList = "INSERT INTO list (user_id, list_title) " +
            "OUTPUT INSERTED.list_id " +
            "VALUES (@user_id, @list_title) ";
        private readonly string sqlGetList = "SELECT list_id, user_id, list_title FROM list " +
            "WHERE list_id = @list_id;";
        private readonly string sqlListGameListByUserId = "SELECT list_id, user_id, list_title FROM list " +
            "WHERE user_id = @user_id;";

        private readonly string sqlGetGamesByListId = "SELECT game_id FROM game_list WHERE list_id = @list_id;";
        private readonly string sqlDeleteGameIdsByListId = "DELETE game_list WHERE list_id = @list_id;";
        private readonly string sqlAddGameToGameList = "INSERT INTO game_list (game_id, list_id) " +
            "VALUES (@game_id, @list_id);";
        public GameListSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public GameList GetGameList(int listId)
        {
            GameList gamelist = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetList, conn))
                    {
                        cmd.Parameters.AddWithValue("@list_id", listId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                gamelist = MapRowToGameList(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return gamelist;
        }

        public List<GameList> ListGameListsByUserId(int userId)
        {
            List<GameList> gameList = new List<GameList>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListGameListByUserId, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameList list = MapRowToGameList(reader);
                                gameList.Add(list);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return gameList;
        }

        public GameList AddGameList(GameList gameList)
        {
            int listId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddList, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", gameList.UserId);
                        cmd.Parameters.AddWithValue("@list_title", gameList.ListTitle);

                        listId = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return GetGameList(listId);
        }

        public GameList UpdateGameList(GameList gameList)
        {
            try
            {
                GameList prevGameList = GetGameList(gameList.ListId);

                if (prevGameList != null && prevGameList.UserId == gameList.UserId && prevGameList.ListTitle == gameList.ListTitle)
                {
                    DeleteGameIdsByListId(gameList.ListId);
                    AddGameIdsByListId(gameList);

                    return gameList;
                }
                return null;

            }
            catch (SqlException)
            {
                return null;
            }
        }

        private List<int> GetGameIdsByListId(int listId)
        {
            List<int> gameIds = new List<int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetGamesByListId, conn))
                    {
                        cmd.Parameters.AddWithValue("@list_id", listId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int gameId = Convert.ToInt32(reader["game_id"]);
                                gameIds.Add(gameId);

                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<int>();
            }

            return gameIds;
        }

        private bool DeleteGameIdsByListId(int listId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteGameIdsByListId, conn))
                    {
                        cmd.Parameters.AddWithValue("@list_id", listId);
                        return cmd.ExecuteNonQuery() > 0;

                    }
                }


            }
            catch (SqlException)
            {
                return false;
            }

        }

        private bool AddGameIdsByListId(GameList gameList)
        {
            try
            {
                foreach (int gameId in gameList.GameIds)
                {
                    AddGameToGameList(gameId, gameList.ListId);
                }

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private bool AddGameToGameList(int gameId, int listId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGameToGameList, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@list_id", listId);

                        return cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private GameList MapRowToGameList(SqlDataReader reader)
        {

            GameList gameList = new GameList();

            gameList.ListId = Convert.ToInt32(reader["list_id"]);
            gameList.UserId = Convert.ToInt32(reader["user_id"]);
            gameList.ListTitle = Convert.ToString(reader["list_title"]);

            gameList.GameIds = GetGameIdsByListId(gameList.ListId);

            return gameList;
        }
    }
}
