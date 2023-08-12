using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public IReviewDao reviewDao;
        public ICommentDao commentDao;

        public ReviewController(IReviewDao reviewDao, ICommentDao commentDao)
        {
            this.reviewDao = reviewDao;
            this.commentDao = commentDao;
        }


        // Gets a list of reviews for specific game
        [HttpGet("game/{gameId}")]
        public ActionResult<List<Review>> ListReviewsByGameId(int gameId)
        {
            List<Review> reviews = reviewDao.ListReviewsByGameId(gameId);
            if (reviews != null)
            {
                return Ok(reviews);
            }
            else
            {
                return BadRequest();
            }
        }

        //Gets a list of reviews by a specific user
        [HttpGet("reviewer/{reviewerId}")]
        public ActionResult<List<Review>> ListReviewsByReviewerId(int reviewerId)
        {
            List<Review> reviews = reviewDao.ListReviewsByReviewerId(reviewerId);
            if (reviews != null)
            {
                return Ok(reviews);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{reviewID}")]
        public ActionResult<Review> GetReview(int reviewId)
        {
            Review review = reviewDao.GetReview(reviewId);
            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()] //Creates a new review
        public ActionResult<Review> AddReview(Review review)
        {
            Review newReview = reviewDao.AddReview(review);
            if (newReview != null)
            {
                return Ok(newReview);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()] //Edits a specific review
        public ActionResult<Review> UpdateReview(Review review)
        {
            Review newReview = reviewDao.UpdateReview(review);
            if (newReview != null)
            {
                return Ok(newReview);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{reviewId}")]
        public ActionResult<bool> DeleteReview( int reviewId)
        {
            commentDao.DeleteCommentsByReviewId(reviewId);
            bool result = reviewDao.DeleteReview(reviewId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete("game/{reviewId}")]
        public ActionResult<bool> DeleteReviewsByGameId(int gameId)
        {
            List<Review> gameReviews = reviewDao.ListReviewsByGameId(gameId);

            foreach (Review review in gameReviews)
            {
                commentDao.DeleteCommentsByReviewId(review.ReviewId);
            }

            bool result = reviewDao.DeleteReviewsByGameId(gameId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete("reviewer/{reviewerId}")]
        public ActionResult<bool> DeleteReviewsByReviewerId(int reviewerId)
        {
            List<Review> userReviews = reviewDao.ListReviewsByReviewerId(reviewerId);

            foreach (Review review in userReviews)
            {
                commentDao.DeleteCommentsByReviewId(review.ReviewId);
            }

            bool result = reviewDao.DeleteReviewsByReviewerId(reviewerId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }
    }
}