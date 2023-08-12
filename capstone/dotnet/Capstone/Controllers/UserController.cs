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
        public IRatingDao ratingDao;
        public IReviewDao reviewDao;

        public UserController(IUserDao userDao, IRatingDao ratingDao, IReviewDao reviewDao)
        {
            this.userDao = userDao;
            this.ratingDao = ratingDao;
            this.reviewDao = reviewDao;
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

        [HttpDelete("{userId}")]
        public ActionResult<bool> DeleteUser(int userId)
        {
            ratingDao.DeleteRatingsByUserId(userId);
            reviewDao.DeleteReviewsByReviewerId(userId);
            bool result = userDao.DeleteUser(userId);
            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
