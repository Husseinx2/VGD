using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameListController : ControllerBase
    {
        public IGameListDao gameListDao;
        public GameListController(IGameListDao gameListDao)
        {
            this.gameListDao = gameListDao;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<GameList>> ListGameListsByUserId(int userId)
        {
            List<GameList> gameLists = gameListDao.ListGameListsByUserId(userId);
            if (gameLists != null)
            {
                return Ok(gameLists);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{listId}")]
        public ActionResult<List<GameList>> GetGameList(int listId)
        {
            GameList gameList = gameListDao.GetGameList(listId);
            if (gameList != null)
            {
                return Ok(gameList);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost()] //Creates a new game list
        public ActionResult<GameList> AddGameList(GameList gameList)
        {
            GameList newGameList = gameListDao.AddGameList(gameList);
            if (newGameList != null)
            {
                return Ok(newGameList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()] //Edits a specific game list
        public ActionResult<GameList> UpdateGameList(GameList gameList)
        {
            GameList newGameList = gameListDao.UpdateGameList(gameList);
            if (newGameList != null)
            {
                return Ok(newGameList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{listId}")]
        public ActionResult<bool> DeleteGameList(int listId)
        {
            bool result = gameListDao.DeleteGameList(listId);
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
