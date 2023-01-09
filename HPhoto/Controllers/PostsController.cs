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
        private readonly DataContext _db;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(DataContext db, IMapper mapper,
            IPostService postService)
        {
            _db = db;
            _mapper = mapper;
            _postService = postService;
        }

        // Get all posts
        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {
            try
            {
                var result = await _db.Posts
                    .Select(x => new Post()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        UserId = x.UserId,
                        TagId = x.TagId,
                        CreatedDate = x.CreatedDate,
                        ImgPath = x.ImgPath,
                        ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host,
                            Request.PathBase, x.ImgPath)
                    }).ToListAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetById([FromRoute] int id)
        {
            var post = await _postService.GetById(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<List<Post>>> Create([FromForm] PostUpsertRequest input)
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

            dbPost.Description = post.Description;
            dbPost.CreatedDate = DateTime.Now;
            dbPost.ImgPath = post.ImgPath;
            dbPost.ImageFile = post.ImageFile;
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
