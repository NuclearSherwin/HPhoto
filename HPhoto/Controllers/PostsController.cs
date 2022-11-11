﻿using AutoMapper;
using HPhoto.Data;
using HPhoto.Dtos.TagDto;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(DataContext dataContext, IPostService postService, IMapper mapper)
        {
            _dataContext = dataContext;
            _postService = postService;
            _mapper = mapper;
        }

        // Get all posts
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {
            return Ok(await _postService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetById([FromRoute] int id)
        {
            var post = await _postService.GetById(id);
            return Ok(post);
        }

        // Create a tag
        [HttpPost]
        public async Task<ActionResult<List<Post>>> CreatePost(PostUpsertRequest input)
        {
            var mappedPost = _mapper.Map<Post>(input);
            await _postService.Create(mappedPost);

            return Ok(mappedPost);
        }

        // Update a post
        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<Post>>> UpdatePost([FromRoute] int id, PostUpsertRequest post)
        {
            var dbPost = await _postService.GetById(id);

            dbPost.Title = post.Title;
            dbPost.Description = post.Description;
            dbPost.CreatedDate = DateTime.Now;
            dbPost.ImgPath = post.ImgPath;
            dbPost.UserId = post.UserId;
            dbPost.TagId = post.TagId;

            //_dataContext.Posts.Update(dbPost);
            await _postService.Update(dbPost);

            return Ok(dbPost);
        }

        // Delete a post
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Post>>> DeletePost(Post post)
        {
            var dbPost = _dataContext.Posts.FindAsync(post.Id);
            if (dbPost == null)
                return BadRequest("Post not found.");

            _dataContext.Posts.Remove(post);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Posts.ToListAsync());
        }

    }
}
