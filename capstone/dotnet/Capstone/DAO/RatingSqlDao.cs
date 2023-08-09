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

        private readonly string sqlListRatings = "SELECT ratings_id, game_id, user_id, rating_value, rating_datetime FROM ratings";
        private readonly string sqlGetRatings = "SELECT ratings_id, game_id, user_id, rating_value, rating_datetime FROM ratings " +
            "WHERE ratings_id = @ratings_id;";
        private readonly string sqlAddRatings = "INSERT INTO ratings (game_id, user_id, rating_value, rating_datetime) " +
            "OUTPUT INSERTED.ratings_id " +
            "VALUES (@game_id, @user_id, @rating_value, @rating_datetime) ";
        private readonly string sqlDeleteRatings = "DELETE ratings where ratings.ratings_id = @ratings_id;";
        private readonly string sqlUpdateRatings = "UPDATE ratings SET game_id=@game_id, user_id=@user_id, rating_value=@rating_value, " +
             "rating_datetime=@rating_datetime " +
            "WHERE ratings_id = @ratings_id;";
        public List<Rating> ListRatingsByGameId(int ratingId)
        {
            List<Rating> ratings = new List<Rating>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListRatings, conn))
                    {
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
            rating.RatingId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddRatings, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", rating.GameId);
                        cmd.Parameters.AddWithValue("@user_id", rating.UserId);
                        cmd.Parameters.AddWithValue("@rating_value", rating.Value);
                        cmd.Parameters.AddWithValue("@rating_datetime", rating.DatePosted);

                        rating.RatingId = (int)cmd.ExecuteScalar();
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
                    using (SqlCommand cmd = new SqlCommand(sqlGetRatings, conn))
                    {
                        cmd.Parameters.AddWithValue("@ratings_id", ratingId);
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

        public bool DeleteRating(int ratingId)
        {
            throw new System.NotImplementedException();
        }

        public Rating UpdateRating(int ratingId, int userId)
        {
            throw new System.NotImplementedException();
        }

        private Rating MapRowToRating(SqlDataReader reader)
        {

            Rating rating = new Rating();

            rating.RatingId = Convert.ToInt32(reader["ratings_id"]);
            rating.GameId = Convert.ToInt32(reader["game_id"]);
            rating.UserId = Convert.ToInt32(reader["user_id"]);
            rating.Value = Convert.ToInt32(reader["rating_value"]);
            rating.DatePosted = Convert.ToDateTime(reader["rating_datetime"]);
            return rating;
        }
    }
}
