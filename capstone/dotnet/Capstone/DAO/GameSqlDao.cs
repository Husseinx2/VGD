using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Policy;

namespace Capstone.DAO
{
    public class GameSqlDao : IGameDao
    {
        private readonly string connectionString = "";

        private readonly string sqlListGames = "SELECT game_id, title, description, esrb_rating, release_date, image_url FROM game";
        private readonly string sqlGetGame = "SELECT game_id, title, description, esrb_rating, release_date, image_url from game WHERE game_id = @game_id;";

        private readonly string sqlAddGame = "INSERT INTO game (title, description, esrb_rating, release_date, image_url) " +
            "OUTPUT INSERTED.game_id " +
            "VALUES (@title, @description, @esrb_rating, @release_date, @image_url) ";

        private readonly string sqlAddGameGenre = "INSERT INTO game_genre (game_id, genre_id) " +
            "VALUES (@game_id, @genre_id) ";
        private readonly string sqlAddGamePlatform = "INSERT INTO game_platform (game_id, platform_id) " +
            "VALUES (@game_id, @platform_id) ";
        private readonly string sqlAddGameDeveloper = "INSERT INTO game_developer (game_id, developer_id) " +
            "VALUES (@game_id, @developer_id) ";
        private readonly string sqlAddGamePublisher = "INSERT INTO game_publisher (game_id, publisher_id) " +
            "VALUES (@game_id, @publisher_id) ";

        private readonly string sqlAddGenre = "INSERT INTO genre (genre_name) " +
           "VALUES (@genre_name) ";
        private readonly string sqlAddPlatform = "INSERT INTO platform (platform_name) " +
           "VALUES (@platform_name) ";
        private readonly string sqlAddCompany = "INSERT INTO company (company_name) " +
           "VALUES (@company_name) ";

        private readonly string sqlGetGenresById = "SELECT genre_name FROM game " +
           "JOIN game_genre ON game.game_id = game_genre.game_id " +
           "JOIN genre ON genre.genre_id = game_genre.genre_id " +
           "WHERE game.game_id = @game_id;";
        private readonly string sqlGetPlatformsById = "SELECT platform_name FROM game " +
            "JOIN game_platform ON game_platform.game_id = game.game_id " +
            "JOIN platform ON platform.platform_id = game_platform.platform_id " +
            "WHERE game.game_id = @game_id;";
        private readonly string sqlGetDevelopersById = "SELECT company_name FROM game " +
            "JOIN game_developer ON game.game_id = game_developer.game_id " +
            "JOIN company ON game_developer.developer_id = company.company_id " +
            "WHERE game.game_id = @game_id;";
        private readonly string sqlGetPublishersById = "SELECT company_name FROM game " +
            "JOIN game_publisher ON game.game_id = game_publisher.game_id " +
            "JOIN company ON game_publisher.publisher_id = company.company_id " +
            "WHERE game.game_id = @game_id;";

        private readonly string sqlGetGenreIdByName = "SELECT genre_id FROM genre " +
           "WHERE genre_name = @genre_name;";
        private readonly string sqlGetPlatformIdByName = "SELECT platform_id FROM platform " +
            "WHERE platform_name = @platform_name;";
        private readonly string sqlGetCompanyIdByName = "SELECT company_id FROM company " +
            "WHERE company_name = @company_name;";
        
        private readonly string sqlUpdateGame = "UPDATE game SET title=@title, description=@description, esrb_rating=@esrb_rating, " +
             "release_date=@release_date, image_url=@image_url " +
            "WHERE game_id = @game_id;";

        private readonly string sqlDeleteGame = "DELETE game where game.game_id = @game_id;";

        private readonly string sqlDeleteGameJunction = "DELETE game_genre WHERE game_genre.game_id = @game_id;" +
            "DELETE game_publisher WHERE game_publisher.game_id = @game_id;" +
            "DELETE game_developer WHERE game_developer.game_id = @game_id;" +
            "DELETE game_platform WHERE game_platform.game_id = @game_id;";

        private readonly string sqlListGenres = "SELECT genre_name FROM genre;";

        private readonly string sqlListPlatforms = "SELECT platform_name FROM platform;";

        private readonly string sqlListCompanies = "SELECT company_name FROM company;";

        private readonly string sqlSearch = "SELECT DISTINCT game.game_id, title, description, esrb_rating, release_date, image_url FROM game " +
            "JOIN game_genre ON game.game_id = game_genre.game_id " +
            "JOIN genre ON game_genre.genre_id = genre.genre_id " +
            "JOIN game_platform ON game.game_id = game_platform.game_id " +
            "JOIN platform ON game_platform.platform_id = platform.platform_id " +
            "JOIN game_developer ON game.game_id = game_developer.game_id " +
            "JOIN company AS developer ON game_developer.developer_id = developer.company_id " +
            "JOIN game_publisher ON game.game_id = game_publisher.game_id " +
            "JOIN company AS publisher ON game_publisher.publisher_id = publisher.company_id " +
            "WHERE (@title = '' OR title LIKE @title) " +
            "AND (@esrb_rating = '' OR esrb_rating = @esrb_rating) " +
            "AND (@year = 0 OR YEAR(release_date) = @year) " +
            "AND (@genre_name = '' OR genre_name = @genre_name) " +
            "AND (@platform_name = '' OR platform_name = @platform_name) " +
            "AND (@developer_name = '' OR developer.company_name = @developer_name) " +
            "AND (@publisher_name = '' OR publisher.company_name = @publisher_name) ";

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
                        cmd.Parameters.AddWithValue("@image_url", game.ImageUrl);

                        game.Id = (int)cmd.ExecuteScalar();
                    }

                    AddGameJunction(game);
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
                        cmd.Parameters.AddWithValue("@image_url", game.ImageUrl);

                        int count = cmd.ExecuteNonQuery();

                        DeleteGameJunction(game.Id);
                        AddGameJunction(game);

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

        public int GetGenreIdByName(string genreName)
        {
            int genreId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetGenreIdByName, conn))
                    {
                        cmd.Parameters.AddWithValue("@genre_name", genreName);
                        genreId = (int)(cmd.ExecuteScalar() ?? 0);
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }

            return genreId;
        }

        public int GetPlatformIdByName(string platformName)
        {
            int platformId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetPlatformIdByName, conn))
                    {
                        cmd.Parameters.AddWithValue("@platform_name", platformName);
                        platformId = (int)(cmd.ExecuteScalar() ?? 0);
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }

            return platformId;
        }

        public int GetCompanyIdByName(string companyName)
        {
            int companyId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetCompanyIdByName, conn))
                    {
                        cmd.Parameters.AddWithValue("@company_name", companyName);
                        companyId = (int)(cmd.ExecuteScalar() ?? 0);
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }

            return companyId;
        }

        public bool AddGameGenre(int gameId, int genreId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGameGenre, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@genre_id", genreId);
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

        public bool AddGamePlatform(int gameId, int platformId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGamePlatform, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@platform_id", platformId);
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

        public bool AddGameDeveloper(int gameId, int developerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGameDeveloper, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@developer_id", developerId);
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

        public bool AddGamePublisher(int gameId, int publisherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGamePublisher, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@publisher_id", publisherId);
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

        public bool AddGenre(string genreName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddGenre, conn))
                    { 
                        cmd.Parameters.AddWithValue("@genre_name", genreName);
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

        public bool AddPlatform(string platformName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddPlatform, conn))
                    {
                        cmd.Parameters.AddWithValue("@platform_name", platformName);
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

        public bool AddCompany(string companyName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddCompany, conn))
                    {
                        cmd.Parameters.AddWithValue("@company_name", companyName);
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

        public bool AddGameJunction(Game game)
        {
            try
            {
                foreach (string genreName in game.Genres)
                {
                    int genreId = GetGenreIdByName(genreName);

                    if (genreId == 0)
                    {
                        AddGenre(genreName);
                        genreId = GetGenreIdByName(genreName);
                    }

                    AddGameGenre(game.Id, genreId);
                }

                foreach (string platformName in game.Platforms)
                {
                    int platformId = GetPlatformIdByName(platformName);

                    if (platformId == 0)
                    {
                        AddPlatform(platformName);
                        platformId = GetPlatformIdByName(platformName);
                    }

                    AddGamePlatform(game.Id, platformId);
                }

                foreach (string developerName in game.Developers)
                {
                    int developerId = GetCompanyIdByName(developerName);

                    if (developerId == 0)
                    {
                        AddCompany(developerName);
                        developerId = GetCompanyIdByName(developerName);
                    }

                    AddGameDeveloper(game.Id, developerId);
                }

                foreach (string publisherName in game.Publishers)
                {
                    int publisherId = GetCompanyIdByName(publisherName);

                    if (publisherId == 0)
                    {
                        AddCompany(publisherName);
                        publisherId = GetCompanyIdByName(publisherName);
                    }

                    AddGamePublisher(game.Id, publisherId);
                }
            }
            catch (SqlException)
            {
                return false;
            }
       
            return true;
        }
        public bool DeleteGameJunction(int gameId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteGameJunction, conn))
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

        public List<string> ListGenres()
        {
            List<string> genres = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListGenres, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                genres.Add(Convert.ToString(reader["genre_name"]));
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

        public List<string> ListPlatforms()
        {
            List<string> platforms = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListPlatforms, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                platforms.Add(Convert.ToString(reader["platform_name"]));
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

        public List<string> ListCompanies()
        {
            List<string> companies = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListCompanies, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                companies.Add(Convert.ToString(reader["company_name"]));
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<string>();
            }

            return companies;
        }

        public List<Game> Search(SearchParameters searchParams)
        {
            List<Game> games = new List<Game>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlSearch, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", $"%{searchParams.Title}%");
                        cmd.Parameters.AddWithValue("@year", searchParams.Year);
                        cmd.Parameters.AddWithValue("@esrb_rating", searchParams.ESRBRating);
                        cmd.Parameters.AddWithValue("@genre_name", searchParams.GenreName);
                        cmd.Parameters.AddWithValue("@platform_name", searchParams.PlatformName);
                        cmd.Parameters.AddWithValue("@developer_name", searchParams.DeveloperName);
                        cmd.Parameters.AddWithValue("@publisher_name", searchParams.PublisherName);

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

        private Game MapRowToGame(SqlDataReader reader)
        {

            Game game = new Game();

            game.Id = Convert.ToInt32(reader["game_id"]);
            game.Title = Convert.ToString(reader["title"]);
            game.Description = Convert.ToString(reader["description"]);
            game.ESRBRating = Convert.ToString(reader["esrb_rating"]);
            game.ReleaseDate = Convert.ToDateTime(reader["release_date"]);
            game.ImageUrl = Convert.ToString(reader["image_url"]);

            game.Genres = GetGenresById(game.Id);
            game.Platforms = GetPlatformsById(game.Id);
            game.Developers = GetDevelopersById(game.Id);
            game.Publishers = GetPublishersById(game.Id);
            return game;
        }
    }
}
