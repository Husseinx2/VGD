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

        private readonly string sqlGetList = "SELECT game_id, user_id, game_list_type, game_list_datetime FROM game_list " +
            "WHERE user_id = @user_id AND game_list_type = @game_list_type;";

        private readonly string sqlAddGameToList = "INSERT INTO game_list (game_id, user_id, game_list_type, game_list_datetime) " +
            "VALUES (@game_id, @user_id, @game_list_type, GETDATE());";

        private readonly string sqlDeleteGameFromList = "DELETE game_list WHERE game_id = @game_id AND user_id = @user_id AND game_list_type = @game_list_type;";

        public GameListSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<GameListEntry> GetGameList(int userId, string gameListType)
        {
            List<GameListEntry> gamelist = new List<GameListEntry>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetList, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@game_list_type", gameListType);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameListEntry entry = MapRowToGameListEntry(reader);
                                gamelist.Add(entry);
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

        public bool AddGameToList(GameListEntry entry)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGameToList, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", entry.GameId);
                        cmd.Parameters.AddWithValue("@user_id", entry.UserId);
                        cmd.Parameters.AddWithValue("@game_list_type", entry.GameListType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteGameFromList(GameListEntry entry)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteGameFromList, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", entry.GameId);
                        cmd.Parameters.AddWithValue("@user_id", entry.UserId);
                        cmd.Parameters.AddWithValue("@game_list_type", entry.GameListType);

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

  

        private GameListEntry MapRowToGameListEntry(SqlDataReader reader)
        {

            GameListEntry gameList = new GameListEntry();

            gameList.GameId = Convert.ToInt32(reader["game_id"]);
            gameList.UserId = Convert.ToInt32(reader["user_id"]);
            gameList.GameListType = Convert.ToString(reader["game_list_type"]);
            gameList.GameListDatetime = Convert.ToDateTime(reader["game_list_datetime"]);

            return gameList;
        }
    }
}
