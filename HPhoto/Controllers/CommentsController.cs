﻿using AutoMapper;
using HPhoto.Data;
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
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentService.GetById(id);
            return Ok(comment);
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
