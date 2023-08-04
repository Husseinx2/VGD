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
        private string sqlGetGame = "SELECT game_id, title, description, esrb_rating, release_date from game WHERE game_id = @id;";
        private string sqlAddGame = "INSERT INTO game (title, description, esrb_rating, release_date) " +
            "OUTPUT INSERTED.id " +
            "VALUES (@title, @description, @esrb_rating, @release_date);";
        private string sqlUpdateGame = "UPDATE game SET title=@title, description=@description, esrb_rating=@esrb_rating, " +
            "release_date=@release_date " +
            "WHERE id = @id;";
        private string sqlDeleteGame = "DELETE FROM game WHERE game_id=@id;";

        public GameSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Game> ListGames()
        {
            List<Game> games = new List<Game>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlListGames, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Game game = new Game();
                            game = MapRowToGame(reader);
                            games.Add(game);
                        }
                    }
                }
            }
            return games;
        }

        public Game GetGame(int gameId)
        {
            Game game = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlGetGame, conn))
                {
                    cmd.Parameters.AddWithValue("@id", gameId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = MapRowToGame(reader);
                        }
                    }
                }
            }
            return game;
        }

        public Game AddGame(Game newGame)
        {
            newGame.Id = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlAddGame, conn))
                {
                    cmd.Parameters.AddWithValue("@title", newGame.Title);
                    cmd.Parameters.AddWithValue("@description", newGame.Description);
                    cmd.Parameters.AddWithValue("@esrb_rating", newGame.ESRBRating);
                    cmd.Parameters.AddWithValue("@release_date", newGame.ReleaseDate);

                    newGame.Id = (int)cmd.ExecuteScalar();
                }
            }

            return GetGame(newGame.Id);
        }

        public Game UpdateGame(Game game)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlUpdateGame, conn))
                {
                    cmd.Parameters.AddWithValue("@title", game.Title);
                    cmd.Parameters.AddWithValue("@description", game.Description);
                    cmd.Parameters.AddWithValue("@type", game.ESRBRating);
                    cmd.Parameters.AddWithValue("@esrb_rating", game.ReleaseDate);

                    int count = cmd.ExecuteNonQuery();

                    return  count == 1 ? game : null;
                }
            }
        }


        public bool DeleteGame(int gameId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlDeleteGame, conn))
                {
                    cmd.Parameters.AddWithValue("@id", gameId);

                    int count = cmd.ExecuteNonQuery();
                    return count == 1;
                }
            }
        }

        private Game MapRowToGame(SqlDataReader reader)
        {
            Game game = new Game();
            game.Id = Convert.ToInt32(reader["game_id"]);
            game.Title = Convert.ToString(reader["title"]);
            game.Description = Convert.ToString(reader["description"]);
            game.ESRBRating = Convert.ToString(reader["esrb_rating"]);
            game.ReleaseDate = Convert.ToDateTime(reader["release_date"]);
            return game;
        }
        
    }
}
