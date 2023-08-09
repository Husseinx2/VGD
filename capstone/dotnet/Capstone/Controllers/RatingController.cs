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

        [HttpGet("game/{gameId}")]
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

        [HttpGet("user/{userId}")]
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

        [HttpGet("/GetRating/{gameId}/{userId}")]
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

        [HttpPost()]
        public ActionResult<Rating> AddRating(Rating rating)
        {
            rating.RatingId = 0;
            Rating newRating = ratingDao.AddRating(rating);
            if (newRating != null || newRating.RatingId == 0)
            {
                return Ok(newRating);
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()]
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

        [HttpDelete("/{gameId}")]
        public ActionResult<bool> DeleteRating(int gameId)
        {
            bool result =ratingDao.DeleteRating(gameId);
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
