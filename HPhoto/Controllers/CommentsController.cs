using HPhoto.Data;
using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CommentsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get all comment
        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAll()
        {
            return Ok(_dataContext.Comments.ToListAsync());
        }


        // Create a comment
        [HttpPost]
        public async Task<ActionResult<List<Comment>>> CreateComment(Comment comment)
        {
            _dataContext.Comments.Add(comment);
            await _dataContext.SaveChangesAsync();
            return Ok(_dataContext.Comments.ToListAsync());
        }

        // Update a comment
        [HttpPut]
        public async Task<ActionResult<List<Comment>>> UpdateComment(Comment comment)
        {
            var dbComment = await _dataContext.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);
            if (dbComment == null)
            {
                return BadRequest("Comment not found.");
            }

            dbComment.Content = comment.Content;
            dbComment.Created = comment.Created;
            dbComment.PostId = comment.PostId;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Comments.ToListAsync());
        }

        // Delete a comment
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Comment>>> DeleteComment(int id)
        {
            var dbComment = await _dataContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbComment == null)
                return BadRequest("Comment not found.");

            _dataContext.Comments.Remove(dbComment);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Comments.ToListAsync());
        }

    }
}
