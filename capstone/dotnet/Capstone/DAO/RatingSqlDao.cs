using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class RatingSqlDao : IRatingDao
    {
        private readonly string connectionString = "";

        private readonly string sqlListRatingsByGameId = "SELECT  game_id, user_id, rating_value, rating_datetime FROM rating  WHERE rating.game_id = @game_id";
        private readonly string sqlListRatingsByUserId = "SELECT  game_id, user_id, rating_value, rating_datetime FROM rating WHERE rating.user_id = @user_id";

        private readonly string sqlGetRating = "SELECT rating_id, game_id, user_id, rating_value, rating_datetime FROM rating " +
            "WHERE rating_id = @rating_id;";
        private readonly string sqlAddRating = "INSERT INTO rating (game_id, user_id, rating_value, rating_datetime) " +
            "OUTPUT INSERTED.rating_id " +
            "VALUES (@game_id, @user_id, @rating_value, @rating_datetime) ";
        private readonly string sqlDeleteRating = "DELETE rating where rating.game_id = @game_id AND rating.user_id = @user_id";
        private readonly string sqlUpdateRating = "UPDATE rating SET game_id=@game_id, user_id=@user_id, rating_value=@rating_value, " +
             "rating_datetime=@rating_datetime " +
            "WHERE rating_id = @ratings_id;";

        public RatingSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Rating> ListRatingsByGameId(int gameId)
        {
            List<Rating> ratings = new List<Rating>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListRatingsByGameId, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rating rating = MapRowToRating(reader);
                                ratings.Add(rating);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return ratings;
        }
        public List<Rating> ListRatingsByUserId(int userId)
        {
            List<Rating> ratings = new List<Rating>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListRatingsByUserId, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rating rating = MapRowToRating(reader);
                                ratings.Add(rating);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return ratings;
        }
        public Rating AddRating(Rating rating)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddRating, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", rating.GameId);
                        cmd.Parameters.AddWithValue("@user_id", rating.UserId);
                        cmd.Parameters.AddWithValue("@rating_value", rating.Value);
                        cmd.Parameters.AddWithValue("@rating_datetime", rating.DatePosted);

                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return rating;
        }

        public Rating GetRating(int ratingId, int userId)
        {
            Rating rating = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetRating, conn))
                    {
                        cmd.Parameters.AddWithValue("@rating_id", ratingId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rating = MapRowToRating(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return rating;
        }

        public bool DeleteRating(int gameId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteRating, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@user_id", userId);


                        int count = cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }


        public Rating UpdateRating(Rating rating)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateRating, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", rating.GameId);
                        cmd.Parameters.AddWithValue("@user_id", rating.UserId);
                        cmd.Parameters.AddWithValue("@rating_value", rating.Value);
                        cmd.Parameters.AddWithValue("@rating_datetime", rating.DatePosted);

                        int count = cmd.ExecuteNonQuery();

                        return rating;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }
        public List<Rating> ListRatingByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        private Rating MapRowToRating(SqlDataReader reader)
        {

            Rating rating = new Rating();

            rating.GameId = Convert.ToInt32(reader["game_id"]);
            rating.UserId = Convert.ToInt32(reader["user_id"]);
            rating.Value = Convert.ToInt32(reader["rating_value"]);
            rating.DatePosted = Convert.ToDateTime(reader["rating_datetime"]);
            return rating;
        }

        
    }
}
