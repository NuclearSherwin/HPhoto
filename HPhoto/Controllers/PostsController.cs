using AutoMapper;
using HPhoto.Data;
using HPhoto.Dtos.PostDto;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        // private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PostsController(IPostService postService)
        {
            _postService = postService;
            // _mapper = mapper;
            // _logger = logger;
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

        // Create a post
        // [HttpPost]
        // public async Task<ActionResult<List<Post>>> CreatePost(PostUpsertRequest input)
        // {
        //     var mappedPost = _mapper.Map<Post>(input);
        //     await _postService.Create(mappedPost);
        //
        //     return Ok(mappedPost);
        // }
        
        // Create a post
        [HttpPost]
        [Route("")]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> SubmitPost([FromForm] PostUpsertRequest postUpsertRequest)
        {
            if (postUpsertRequest == null)
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorCode = "501",
                    Error = "Invalid post request"
                });
            }

            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
            {
                return BadRequest(new PostResponse
                {
                    Success = false,
                    ErrorCode = "502",
                    Error = "Invalid post header"
                });
            }

            if (postUpsertRequest.Image != null)
            {
                await _postService.SavePostImageAsync(postUpsertRequest);
            }

            var postResponse = await _postService.CreatePostAsync(postUpsertRequest);
            if (!postResponse.Success)
            {
                return NotFound(postResponse);
            }

            return Ok(postResponse.Post);
        }
        

        // Update a post
        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<Post>>> UpdatePost([FromRoute] int id, PostUpsertRequest post)
        {
            var dbPost = await _postService.GetById(id);

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
        
    }
}
