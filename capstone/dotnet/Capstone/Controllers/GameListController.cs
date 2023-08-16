using Capstone.DAO.Interfaces;
using Capstone.Models;
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

        [HttpGet("{userId}/{gameListType}")]
        public ActionResult<List<GameListEntry>> GetGameList(int userId, string gameListType)
        {
            List<GameListEntry> gameList = gameListDao.GetGameList(userId, gameListType);
            if (gameList != null)
            {
                return Ok(gameList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()]
        public ActionResult<bool> AddGameToList(GameListEntry entry)
        {
            bool result = gameListDao.AddGameToList(entry);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete()]
        public ActionResult<bool> DeleteGameFromList(GameListEntry entry)
        {
            bool result = gameListDao.DeleteGameFromList(entry);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
