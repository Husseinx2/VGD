using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ReviewSqlDao : IReviewDAO
    {
        private readonly string connectionString = "";

        private readonly string sqlListReviewsByGameId = "SELECT  game_id, reviewer_id, review_content, review_datetime FROM review WHERE review.game_id = @game_id;";

        private readonly string sqlListReviewsByReviewerId = "SELECT  game_id, reviewer_id, review_content, review_datetime FROM review " +
            "WHERE review.reviewer_id = @reviewer_id;";

        private readonly string sqlGetReview = "SELECT game_id, reviewr_id, review_content, review_datetime FROM review " +
            "WHERE game_id = @game_id AND reviewer_id = @reviewer_id;";

        private readonly string sqlAddReview = "INSERT INTO review (game_id, reviewer_id, review_content, review_datetime) " +
            "VALUES (@game_id, @reviewer_id, @review_content, @review_datetime) ";

        private readonly string sqlUpdateReview = "UPDATE review SET review_content=@review_content, " +
             "review_datetime=@review_datetime " +
            "WHERE game_id = @game_id AND reviewer_id = @reviewer_id;";

        private readonly string sqlDeleteReview = "DELETE review where game_id = @game_id AND reviewer_id = @reviewer_id";

        private readonly string sqlDeleteReviewsByGameId = "DELETE review where game_id = @game_id";

        private readonly string sqlDeleteReviewsByReviewId = "DELETE review where review_id = @review_id";

        public Review AddReview(Review review)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", review.GameId);
                        cmd.Parameters.AddWithValue("@user_id", review.ReviewerId);
                        cmd.Parameters.AddWithValue("@review_value", review.ReviewContent);
                        cmd.Parameters.AddWithValue("@review_datetime", review.ReviewDateTime);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return review;
        }

        public bool DeleteReview(int gameId, int reviewerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@reviewer_id", reviewerId);


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

        public bool DeleteReviewsByGameId(int gameId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReviewsByReviewerId(int reviewerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteReviewsByReviewerId, conn))
                    {
                        cmd.Parameters.AddWithValue("@reviewer_id", reviewerId);

                        int count = cmd.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public Review GetReview(int gameId, int reviewerId)
        {
            Review review = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);
                        cmd.Parameters.AddWithValue("@reviewer_id", reviewerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                review = MapRowToReview(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return review;
        }

        public List<Review> ListReviewsByGameId(int gameId)
        {
            List<Review> reviews = new List<Review>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListReviewsByGameId, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", gameId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Review review = MapRowToReview(reader);
                                reviews.Add(review);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return reviews;
        }

        public List<Review> ListReviewsByReviewerId(int reviewerId)
        {
            List<Review> reviews = new List<Review>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListReviewsByReviewerId, conn))
                    {
                        cmd.Parameters.AddWithValue("@reviewer_id", reviewerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Review review = MapRowToReview(reader);
                                reviews.Add(review);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return reviews;
        }

        public Review UpdateReview(Review review)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", review.GameId);
                        cmd.Parameters.AddWithValue("@user_id", review.ReviewerId);
                        cmd.Parameters.AddWithValue("@review_value", review.ReviewContent);
                        cmd.Parameters.AddWithValue("@review_datetime", review.ReviewDateTime);

                        int count = cmd.ExecuteNonQuery();

                        return review;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        private Review MapRowToReview(SqlDataReader reader)
        {

            Review review = new Review();

            review.GameId = Convert.ToInt32(reader["game_id"]);
            review.ReviewerId = Convert.ToInt32(reader["reviwer_id"]);
            review.ReviewContent = Convert.ToString(reader["review_content"]);
            review.ReviewDateTime = Convert.ToDateTime(reader["review_datetime"]);

            return review;
        }
    }
}
