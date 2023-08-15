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

        private readonly string sqlAddList = "INSERT INTO list (user_id, list_title, list_type_id) " +
            "OUTPUT INSERTED.list_id " +
            "VALUES (@user_id, @list_title, @list_type_id) ";
        private readonly string sqlDeleteList = "DELETE list WHERE list_id = @list_id";
        private readonly string sqlGetList = "SELECT user_id, list_title, list_type_id FROM list " +
            "WHERE list_id = @list_id;";
        private readonly string sqlListGameListByUserId = "SELECT list_id, user_id, list_title, list_type_id FROM list " +
            "WHERE list.list_id = @list_id;";
        private readonly string sqlUpdateList = "UPDATE list SET list_title=@list_title, list_type_id=@list_type_id, " +
            "WHERE list_id = @list_id;";
        private readonly string sqlListGames = "SELECT game_title FROM game;";

        public GameListSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
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

        public bool DeleteGameList(int listId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteList, conn))
                    {
                        cmd.Parameters.AddWithValue("@list_id", listId);

                        int count = cmd.ExecuteNonQuery();
                        return count == 1;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
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
                                gamelist = MapRowToGameList (reader);
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

        public GameList UpdateGameList(GameList gameList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateList, conn))
                    {
                        cmd.Parameters.AddWithValue("@list_id", gameList.ListId);
                        cmd.Parameters.AddWithValue("@user_id", gameList.UserId);
                        cmd.Parameters.AddWithValue("@list_title", gameList.ListTitle);

                        int count = cmd.ExecuteNonQuery();

                        return count == 1 ? gameList : null;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<string> ListGames()
        {
            List<string> games = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListGames, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                games.Add(Convert.ToString(reader["game_title"]));
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return games;
        }

        private GameList MapRowToGameList (SqlDataReader reader)
        {

            GameList gameList = new GameList();

            gameList.ListId = Convert.ToInt32(reader["list_id"]);
            gameList.UserId = Convert.ToInt32(reader["user_id"]);
            gameList.ListTitle = Convert.ToString(reader["list_title"]);

            return gameList;
        }
    }
}
