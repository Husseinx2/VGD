using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public IReviewDAO reviewDao;

        public ReviewController(IReviewDAO reviewDao)
        {
            this.reviewDao = reviewDao;
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
        [HttpGet("review/{reviewId}")]
        public ActionResult<List<Review>> ListReviewsByReviewId(int reviewId)
        {
            List<Review> reviews = reviewDao.ListReviewsByReviewId(reviewId);
            if (reviews != null)
            {
                return Ok(reviews);
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

    }
}