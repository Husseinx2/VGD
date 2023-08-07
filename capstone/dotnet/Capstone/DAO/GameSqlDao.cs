using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class GameSqlDao : IGameDao
    {
        private string connectionString = "";

        private string sqlListGames = "SELECT game_id, title, description, esrb_rating, release_date FROM game";
        private string sqlGetGame = "SELECT game_id, title, description, esrb_rating, release_date from game WHERE game_id = @game_id;";
        private string sqlAddGame = "INSERT INTO game (title, description, esrb_rating, release_date) " +
            "OUTPUT INSERTED.game_id " +
            "VALUES (@title, @description, @esrb_rating, @release_date);";
        private string sqlUpdateGame = "UPDATE game SET title=@title, description=@description, esrb_rating=@esrb_rating, " +
            "release_date=@release_date " +
            "WHERE game_id = @game_id;";
        // TODO: Update sqlDeleteGame to delete game with foreign key contraints
        private string sqlDeleteGame = "DELETE FROM game WHERE game_id=@game_id;";

        public GameSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Game> ListGames()
        {
            List<Game> games = new List<Game>();

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
                                Game game = MapRowToGame(reader);
                                games.Add(game);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return games;
        }

        public Game GetGame(int gameId)
        {
            Game game = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = MapRowToGame(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return game;
        }

        public Game AddGame(Game game)
        {
            game.Id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", game.Title);
                        cmd.Parameters.AddWithValue("@description", game.Description);
                        cmd.Parameters.AddWithValue("@esrb_rating", game.ESRBRating);
                        cmd.Parameters.AddWithValue("@release_date", game.ReleaseDate);

                        game.Id = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
            return GetGame(game.Id);
        }

        public Game UpdateGame(Game game)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", game.Id);
                        cmd.Parameters.AddWithValue("@title", game.Title);
                        cmd.Parameters.AddWithValue("@description", game.Description);
                        cmd.Parameters.AddWithValue("@esrb_rating", game.ESRBRating);
                        cmd.Parameters.AddWithValue("@release_date", game.ReleaseDate);

                        int count = cmd.ExecuteNonQuery();

                        return count == 1 ? game : null;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool DeleteGame(int gameId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteGame, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);

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

        public List<string> GetGenresById(int gameId)
        {
            // TODO:
            return new List<string>();
        }

        public List<string> GetPlatformsById(int gameId)
        {
            // TODO:
            return new List<string>();
        }

        public List<string> GetDevelopersById(int gameIde)
        {
            // TODO:
            return new List<string>();
        }

        public List<string> GetPublishersById(int gameId)
        {
            // TODO:
            return new List<string>();
        }

        private Game MapRowToGame(SqlDataReader reader)
        {

            Game game = new Game();

            game.Id = Convert.ToInt32(reader["game_id"]);
            game.Title = Convert.ToString(reader["title"]);
            game.Description = Convert.ToString(reader["description"]);
            game.ESRBRating = Convert.ToString(reader["esrb_rating"]);
            game.ReleaseDate = Convert.ToDateTime(reader["release_date"]);

            game.Genres = GetGenresById(game.Id);
            game.Platforms = GetPlatformsById(game.Id);
            game.Developers = GetDevelopersById(game.Id);
            game.Publishers = GetPublishersById(game.Id);

            return game;
        }
    }
}
