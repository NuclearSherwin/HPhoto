using HPhoto.Data;
using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public TagsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // Get all tags
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetAll()
        {
            return Ok(await _dataContext.Tags.ToListAsync());
        }

        
        // Get detail of tag
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _dataContext.Tags.FindAsync(id);
            if (tag == null)
            {
                return BadRequest("Tag Not found");
            }
            return Ok(tag);
        }
        
        
        // Create a tag
        [HttpPost]
        public async Task<ActionResult<List<Tag>>> CreateTag(Tag tag)
        {
            _dataContext.Tags.Add(tag);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Tags.ToListAsync());
        }

        // Update a tag
        [HttpPut]
        public async Task<ActionResult<List<Tag>>> UpdateTag(Tag tag)
        {
            var dbTag = await _dataContext.Tags.FindAsync(tag.Id);
            if (dbTag == null)
            {
                return BadRequest("Tag not found.");
            }

            dbTag.Name = tag.Name;
            dbTag.Description = tag.Description;
            dbTag.Rating = tag.Rating;

            // save changes
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Tags.ToListAsync());
        }

        // Delete a tag
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Tag>>> DeleteTag(int id)
        {
            var dbTag = await _dataContext.Tags.FindAsync(id);
            if (dbTag == null)
                return BadRequest("Tag not found.");

            _dataContext.Tags.Remove(dbTag);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Tags.ToListAsync());

        }


    }
}
