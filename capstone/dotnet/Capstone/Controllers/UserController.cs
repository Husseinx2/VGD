using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserDao userDao;

        public UserController(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        [HttpGet()]
        public ActionResult<List<ReturnUser>> ListUsers()
        {
            return Ok(userDao.GetUsers());
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
             User user = userDao.GetUserById(userId);
            if (user != null)
            {
                return Ok(userDao.GetUserById(userId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
