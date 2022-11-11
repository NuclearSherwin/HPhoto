using AutoMapper;
using HPhoto.Data;
using HPhoto.Dtos.CommentDto;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(DataContext dataContext, ICommentService commentService, IMapper mapper)
        {
            _dataContext = dataContext;
            _commentService = commentService;
            _mapper = mapper;
        }

        // Get all comment
        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetAll()
        {
            var comments = await _commentService.GetAll();
            return Ok(comments);
        }
        
        // Get comment by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentService.GetById(id);
            return Ok(comment);
        }


        // Create a comment
        [HttpPost]
        public async Task<ActionResult<List<Comment>>> CreateComment(CommentUpsertRequest comment)
        {
            var mappedComment = _mapper.Map<Comment>(comment);
            await _commentService.Create(mappedComment);
            return Ok(mappedComment);
        }

        // Update a comment
        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<Comment>>> UpdateComment([FromRoute] int id, CommentUpsertRequest input)
        {
            var dbComment = await _commentService.GetById(id);

            dbComment.Content = input.Content;
            dbComment.Created = input.Created;
            dbComment.PostId = input.PostId;

            await _commentService.Update(dbComment);
            
            return Ok(dbComment);
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
