using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public ICommentDao commentDao;

        public CommentController(ICommentDao commentDao)
        {
            this.commentDao = commentDao;
        }


        // Gets a list of comments for specific review
        [HttpGet("review/{reviewId}")]
        public ActionResult<List<Comment>> ListCommentsByReviewId(int reviewId)
        {
            List<Comment> comments = commentDao.ListCommentsByReviewId(reviewId);
            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return BadRequest();
            }
        }

        //Gets a list of comments by a specific user
        [HttpGet("commenter/{commenterId}")]
        public ActionResult<List<Comment>> ListCommentsByCommenterId(int commenterId)
        {
            List<Comment> comments = commentDao.ListCommentsByCommenterId(commenterId);
            if (comments != null)
            {
                return Ok(comments);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{commentID}")]
        public ActionResult<Comment> GetComment(int commentId)
        {
            Comment comment = commentDao.GetComment(commentId);
            if (comment != null)
            {
                return Ok(comment);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()] //Creates a new comment
        public ActionResult<Comment> AddComment(Comment comment)
        {
            Comment newComment = commentDao.AddComment(comment);
            if (newComment != null)
            {
                return Ok(newComment);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()] //Edits a specific comment
        public ActionResult<Comment> UpdateComment(Comment comment)
        {
            Comment newComment = commentDao.UpdateComment(comment);
            if (newComment != null)
            {
                return Ok(newComment);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{commentId}")]
        public ActionResult<bool> DeleteComment(int commentId)
        {
            bool result = commentDao.DeleteComment(commentId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete("review/{commentId}")]
        public ActionResult<bool> DeleteCommentsByReviewId(int reviewId)
        {
            bool result = commentDao.DeleteCommentsByReviewId(reviewId);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete("commenter/{commenterId}")]
        public ActionResult<bool> DeleteCommentsByCommenterId(int commenterId)
        {
            bool result = commentDao.DeleteCommentsByCommenterId(commenterId);
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