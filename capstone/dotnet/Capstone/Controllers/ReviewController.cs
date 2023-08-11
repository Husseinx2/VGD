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

        [HttpDelete("{gameId}/{reviewerId}")]
        public ActionResult<bool> DeleteReview(int gameId, int reviewerId)
        {
            bool result = reviewDao.DeleteReview(gameId, reviewerId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete("game/{gameId}")]
        public ActionResult<bool> DeleteReviewsByGameId(int gameId)
        {
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

        [HttpDelete("reviewr/{reviewerId}")]
        public ActionResult<bool> DeleteReviewsByReviewerId(int reviewerId)
        {
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