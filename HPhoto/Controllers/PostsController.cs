﻿using AutoMapper;
using HPhoto.Data;
using HPhoto.Dtos.PostDto;
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
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
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
        [HttpPost(nameof(UploadImage))]
        public async Task<ActionResult<List<Post>>> CreatePost([FromForm]PostUpsertRequest input)
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
        public async Task<ActionResult<List<Post>>> DeletePost([FromRoute] int id)
        {
            var dbPost = await _postService.Delete(id);
            if (!dbPost)
                return BadRequest("Delete failed or not found!.");

            return Ok("Post Deleted successfully!");
        }

        [HttpPost]
        public string UploadImage([FromForm] IFormFile file)
        {
            try
            {
                    // getting file original name
                    string FileName = file.FileName;
                    
                    // combining GUID to create UNIQUE ID to make it unique before saving to wwwroot
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
                    
                    // getting full path inside wwwroot/images
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/images/", FileName);
                    
                    // copying file
                    file.CopyTo(new FileStream(imagePath, FileMode.Create));

                    return "File Uploaded successfully!";
            }   
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

    }
}
