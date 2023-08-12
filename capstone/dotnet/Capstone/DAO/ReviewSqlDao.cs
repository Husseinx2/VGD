using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ReviewSqlDao : IReviewDao
    {
        private readonly string connectionString = "";

        private readonly string sqlListReviewsByGameId = "SELECT  review_id, game_id, reviewer_id, review_content, review_datetime FROM review WHERE review.game_id = @game_id;";

        private readonly string sqlListReviewsByReviewerId = "SELECT review_id, game_id, reviewer_id, review_content, review_datetime FROM review " +
            "WHERE review.reviewer_id = @reviewer_id;";

        private readonly string sqlGetReview = "SELECT review_id, game_id, reviewer_id, review_content, review_datetime FROM review " +
            "WHERE review_id = @review_id";

        private readonly string sqlAddReview = "INSERT INTO review (game_id, reviewer_id, review_content, review_datetime) " +
            "OUTPUT INSERTED.review_id " +
            "VALUES (@game_id, @reviewer_id, @review_content, @review_datetime) ";

        private readonly string sqlUpdateReview = "UPDATE review SET review_content=@review_content, " +
             "review_datetime=@review_datetime " +
            "WHERE review_id = @review_id AND game_id = @game_id AND reviewer_id = @reviewer_id;";
     
        private readonly string sqlDeleteReviewByGameId = "DELETE review WHERE game_id = @game_id;";
        private readonly string sqlDeleteReviewByReviewerId = "DELETE review WHERE reviewer_id = @reviewer_id;";
        private readonly string sqlDeleteReview= "DELETE review WHERE review_id = @review_id";

        public ReviewSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
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

        public Review GetReview(int reviewId)
        {
            Review review = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@review_id", reviewId);

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

        public Review AddReview(Review review)
        {
            Review newReview = null;

            int newReviewId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@game_id", review.GameId);
                        cmd.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
                        cmd.Parameters.AddWithValue("@review_content", review.ReviewContent);
                        cmd.Parameters.AddWithValue("@review_datetime", review.ReviewDateTime);
                        newReviewId = (int)cmd.ExecuteScalar();
                    }

                    newReview = GetReview(newReviewId);
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return newReview;
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
                        cmd.Parameters.AddWithValue("@review_id", review.ReviewId);
                        cmd.Parameters.AddWithValue("@game_id", review.GameId);
                        cmd.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
                        cmd.Parameters.AddWithValue("@review_content", review.ReviewContent);
                        cmd.Parameters.AddWithValue("@review_datetime", review.ReviewDateTime);

                        int count = cmd.ExecuteNonQuery();
                        if (count != 1)
                        {
                            return null;
                        }

                        return review;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool DeleteReview(int reviewId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteReview, conn))
                    {
                        cmd.Parameters.AddWithValue("@review_id", reviewId);
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
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteReviewByGameId, conn))
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
        public bool DeleteReviewsByReviewerId(int reviewerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteReviewByReviewerId, conn))
                    {
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

        private Review MapRowToReview(SqlDataReader reader)
        {
            Review review = new Review();

            review.ReviewId = Convert.ToInt32(reader["review_id"]);
            review.GameId = Convert.ToInt32(reader["game_id"]);
            review.ReviewerId = Convert.ToInt32(reader["reviewer_id"]);
            review.ReviewContent = Convert.ToString(reader["review_content"]);
            review.ReviewDateTime = Convert.ToDateTime(reader["review_datetime"]);

            return review;
        }
    }
}
