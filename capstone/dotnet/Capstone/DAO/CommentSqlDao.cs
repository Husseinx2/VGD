using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class CommentSqlDao : ICommentDao
    {
        private readonly string connectionString = "";

        private readonly string sqlListCommentsByReviewId = "SELECT  comment_id, review_id, commenter_id, comment_content, comment_datetime FROM comment WHERE comment.review_id = @review_id;";

        private readonly string sqlListCommentsByCommenterId = "SELECT comment_id, review_id, commenter_id, comment_content, comment_datetime FROM comment " +
            "WHERE comment.commenter_id = @commenter_id;";

        private readonly string sqlGetComment = "SELECT comment_id, review_id, commenter_id, comment_content, comment_datetime FROM comment " +
            "WHERE comment_id = @comment_id";

        private readonly string sqlAddComment = "INSERT INTO comment (review_id, commenter_id, comment_content, comment_datetime) " +
            "OUTPUT INSERTED.comment_id " +
            "VALUES (@review_id, @commenter_id, @comment_content, @comment_datetime) ";

        private readonly string sqlUpdateComment = "UPDATE comment SET comment_content=@comment_content, " +
             "comment_datetime=@comment_datetime " +
            "WHERE comment_id = @comment_id AND review_id = @review_id AND commenter_id = @commenter_id;";

        private readonly string sqlDeleteCommentByReviewId = "DELETE comment WHERE review_id = @review_id;";
        private readonly string sqlDeleteCommentByCommenterId = "DELETE comment WHERE commenter_id = @commenter_id;";
        private readonly string sqlDeleteComment = "DELETE comment WHERE comment_id = @comment_id";

        public CommentSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Comment> ListCommentsByReviewId(int reviewId)
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListCommentsByReviewId, conn))
                    {
                        cmd.Parameters.AddWithValue("@review_id", reviewId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Comment comment = MapRowToComment(reader);
                                comments.Add(comment);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return comments;
        }

        public List<Comment> ListCommentsByCommenterId(int commenterId)
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlListCommentsByCommenterId, conn))
                    {
                        cmd.Parameters.AddWithValue("@commenter_id", commenterId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Comment comment = MapRowToComment(reader);
                                comments.Add(comment);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return comments;
        }

        public Comment GetComment(int commentId)
        {
            Comment comment = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlGetComment, conn))
                    {
                        cmd.Parameters.AddWithValue("@comment_id", commentId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                comment = MapRowToComment(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return comment;
        }

        public Comment AddComment(Comment comment)
        {
            Comment newComment = null;

            int newCommentId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlAddComment, conn))
                    {
                        cmd.Parameters.AddWithValue("@review_id", comment.ReviewId);
                        cmd.Parameters.AddWithValue("@commenter_id", comment.CommenterId);
                        cmd.Parameters.AddWithValue("@comment_content", comment.CommentContent);
                        cmd.Parameters.AddWithValue("@comment_datetime", comment.CommentDateTime);
                        newCommentId = (int)cmd.ExecuteScalar();
                    }

                    newComment = GetComment(newCommentId);
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return newComment;
        }

        public Comment UpdateComment(Comment comment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateComment, conn))
                    {
                        cmd.Parameters.AddWithValue("@comment_id", comment.CommentId);
                        cmd.Parameters.AddWithValue("@review_id", comment.ReviewId);
                        cmd.Parameters.AddWithValue("@commenter_id", comment.CommenterId);
                        cmd.Parameters.AddWithValue("@comment_content", comment.CommentContent);
                        cmd.Parameters.AddWithValue("@comment_datetime", comment.CommentDateTime);

                        int count = cmd.ExecuteNonQuery();
                        return count == 1 ? comment : null;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool DeleteComment(int commentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteComment, conn))
                    {
                        cmd.Parameters.AddWithValue("@comment_id", commentId);
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

        public bool DeleteCommentsByReviewId(int reviewId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteCommentByReviewId, conn))
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
        public bool DeleteCommentsByCommenterId(int commenterId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteCommentByCommenterId, conn))
                    {
                        cmd.Parameters.AddWithValue("@commenter_id", commenterId);
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

        private Comment MapRowToComment(SqlDataReader reader)
        {
            Comment comment = new Comment();

            comment.CommentId = Convert.ToInt32(reader["comment_id"]);
            comment.ReviewId = Convert.ToInt32(reader["review_id"]);
            comment.CommenterId = Convert.ToInt32(reader["commenter_id"]);
            comment.CommentContent = Convert.ToString(reader["comment_content"]);
            comment.CommentDateTime = Convert.ToDateTime(reader["comment_datetime"]);

            return comment;
        }
    }
}
