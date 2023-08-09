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
<<<<<<< HEAD
            List<Rating> ratings = ratingDao.ListRatingByGameId(gameId);
            if (ratings != null && ratings.Count != 0)
=======
            List<Rating> ratings = ratingDao.ListRatingsByGameId(gameId);
            if (ratings != null)
>>>>>>> fe087d160fb9ba93eff8e66019f65b296849c0e2
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

        [HttpGet("{gameId}/{userId}")]
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
