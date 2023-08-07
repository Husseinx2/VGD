﻿using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private string sqlDeleteGame = "DELETE game_genre WHERE game_genre.game_id = @game_id;" +
            "DELETE game_publisher WHERE game_publisher.game_id = @game_id;" +
            "DELETE game_developer WHERE game_developer.game_id = @game_id;" +
            "DELETE game_platform WHERE game_platform.game_id = @game_id;" +
            "DELETE game where game.game_id = @game_id;";

        private string sqlGetDevelopersById = "SELECT company_name FROM game " +
            "JOIN game_developer ON game.game_id = game_developer.game_id " +
            "JOIN company ON game_developer.developer_id = company.company_id " +
            "WHERE game.game_id = @game_id;";
        private string sqlGetPublishersById = "SELECT company_name FROM game " +
            "JOIN game_publisher ON game.game_id = game_publisher.game_id " +
            "JOIN company ON game_publisher.publisher_id = company.company_id " +
            "WHERE game.game_id = @game_id;";

        private string sqlGetGenresById = "SELECT genre_name FROM game " +
            "JOIN game_genre ON game.game_id = game_genre.game_id " +
            "JOIN genre ON genre.genre_id = game_genre.genre_id " +
            "WHERE game.game_id = @game_id;";

        private string sqlGetPlatformsById = "SELECT platform_name FROM game " +
            "JOIN game_platform ON game_platform.game_id = game.game_id " +
            "JOIN platform ON platform.platform_id = game_platform.platform_id " +
            "WHERE game.game_id = @game_id;";

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
            List<string> genres = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetGenresById, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string item = Convert.ToString(reader["genre_name"]);
                                genres.Add(item);

                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return genres;
        }

        public List<string> GetPlatformsById(int gameId)
        {
            List<string> platforms = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetPlatformsById, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string item = Convert.ToString(reader["platform_name"]);
                                platforms.Add(item);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return platforms;
        }

        public List<string> GetDevelopersById(int gameId)
        {
            List<string> developers = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetDevelopersById, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string item = Convert.ToString(reader["company_name"]);
                                developers.Add(item);
                  
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return developers;
        }

        public List<string> GetPublishersById(int gameId)
        {
            List<string> publishers = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetPublishersById, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string item = Convert.ToString(reader["company_name"]);
                                publishers.Add(item);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return publishers;
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