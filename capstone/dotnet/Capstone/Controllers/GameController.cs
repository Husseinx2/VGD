using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public IGameDao gameDao;
        public IRatingDao ratingDao;

        public GameController(IGameDao gameDao, IRatingDao ratingDao)
        {
            this.gameDao = gameDao;
            this.ratingDao = ratingDao;
        }

        [HttpGet()]
        public ActionResult<List<Game>> GetGames()
        {
            return Ok(gameDao.ListGames());
        }

        [HttpGet("{gameId}")]
        public ActionResult<Game> GetGame(int gameId)
        {
            return Ok(gameDao.GetGame(gameId));
        }

        [HttpPost()]
        public ActionResult<Game> AddGame(Game game)
        {
            Game newGame = gameDao.AddGame(game);

            if (newGame == null || newGame.Id == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newGame);
            }
        }

        [HttpPut("{gameId}")]
        public ActionResult<Game> UpdateGame(Game game)
        {
            Game newGame = gameDao.UpdateGame(game);


            if (newGame == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newGame);
            }
        }

        [HttpDelete("{gameId}")]
        public ActionResult<bool> DeleteGame(int gameId)
        {
            ratingDao.DeleteRatingsByGameId(gameId);
            bool result = gameDao.DeleteGame(gameId);

            if (result)
            {
                return Ok(result);       
            }

            return BadRequest();
        }

        [HttpGet("genres")]
        public ActionResult<List<string>> ListGenres()
        {
            return Ok(gameDao.ListGenres());
        }

        [HttpGet("platforms")]
        public ActionResult<List<string>> ListPlatforms()
        {
            return Ok(gameDao.ListPlatforms());
        }

        [HttpGet("companies")]
        public ActionResult<List<string>> ListCompanies()
        {
            return Ok(gameDao.ListCompanies());
        }
    }
}
