using HPhoto.Data;
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

        public PostsController(DataContext dataContext, IPostService postService)
        {
            _dataContext = dataContext;
            _postService = postService;
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
        public async Task<ActionResult<List<Post>>> CreatePost(Post post)
        {
            _dataContext.Posts.Add(post);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Posts.ToListAsync());
        }

        // Update a post
        [HttpPut]
        public async Task<ActionResult<List<Post>>> UpdatePost(Post post)
        {
            var dbPost = await _dataContext.Posts.FindAsync(post.Id);
            if (dbPost == null)
                return BadRequest("Post not found.");

            dbPost.Title = post.Title;
            dbPost.Description = post.Description;
            dbPost.CreatedDate = DateTime.Now;
            dbPost.ImgPath = post.ImgPath;
            dbPost.UserId = post.UserId;
            dbPost.TagId = post.TagId;

            //_dataContext.Posts.Update(dbPost);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Posts.ToListAsync());
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
