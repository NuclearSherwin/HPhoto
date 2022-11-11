using AutoMapper;
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
    public class TagsController : ControllerBase
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        public TagsController(DataContext dataContext, IMapper mapper, ITagService tagService)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _tagService = tagService;
        }


        // Get all tags
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetAll()
        {
            return Ok(await _tagService.GetAll());
        }

        
        // Get detail of tag
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            var tag = await _tagService.GetTagById(id);
            return Ok(tag);
        }
        
        
        // Create a tag
        [HttpPost]
        public async Task<ActionResult<List<Tag>>> CreateTag(TagUpsertRequest input)
        {
            var mappedTag = _mapper.Map<Tag>(input);
            await _tagService.CreateTag(mappedTag);

            return Ok(mappedTag);
        }

        // Update a tag
        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<Tag>>> UpdateTag([FromRoute] int id, TagUpsertRequest input)
        {
            // it's also validation by GetTagById method;
            var dbTag = await _tagService.GetTagById(id);
            
            dbTag.Name = input.Name;
            dbTag.Description = input.Description;
            dbTag.Rating = input.Rating;

            await _tagService.UpdateTag(dbTag);
            
            return Ok(dbTag);
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
