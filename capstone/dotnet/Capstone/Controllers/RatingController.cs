using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        public IRatingDao ratingDao;

        public RatingController(IRatingDao ratingDao)
        {
            this.ratingDao = ratingDao;
        }

        [HttpGet("game/{gameId}")] //gets a list of ratings for specific game
        public ActionResult<List<Rating>> ListRatingsByGameId(int gameId)
        {
            List<Rating> ratings = ratingDao.ListRatingsByGameId(gameId);
            if (ratings != null)
            {
                return Ok(ratings);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("user/{userId}")] //gets a list of ratings by specific user
        public ActionResult<List<Rating>> ListRatingsByUserId(int userId)
        {
            List<Rating> ratings = ratingDao.ListRatingsByUserId(userId);
            if (ratings != null)
            {
                return Ok(ratings);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{gameId}/{userId}")] //gets a specific rating for a specific game
        public ActionResult<Rating> GetRating(int gameId, int userId)
        {
            Rating rating = ratingDao.GetRating(gameId, userId);
            if (rating != null)
            {
                return Ok(rating);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()] //Creates a new rating
        public ActionResult<Rating> AddRating(Rating rating)
        {
            Rating newRating = ratingDao.AddRating(rating);
            if (newRating != null)
            {
                return Ok(newRating);
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()] //Edits a specific rating
        public ActionResult<Rating> UpdateRating(Rating rating)
        {
            Rating newRating = ratingDao.UpdateRating(rating);
            if (newRating != null)
            {
                return Ok(newRating);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{gameId}/{userId}")]
        public ActionResult<bool> DeleteRating(int gameId,int userId)
        {
            bool result = ratingDao.DeleteRating(gameId,userId);
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
