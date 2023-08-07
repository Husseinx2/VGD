﻿using Capstone.DAO.Interfaces;
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

        public GameController(IGameDao gameDao)
        {
            this.gameDao = gameDao;
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
            return Ok(gameDao.DeleteGame(gameId));
        }
    }
}