using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetAll()
        {
            return new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Name = "Cosmic",
                    Description = "Universe, space, NASA, stars, exploring universe, " +
                                  "black hole, beautiful space",
                    Rating = 1
                }
            };
        }
    }
}
