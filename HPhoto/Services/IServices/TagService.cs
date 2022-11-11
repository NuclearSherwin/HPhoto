using HPhoto.Data;
using HPhoto.Model;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services.IServices;

public class TagService : ITagService
{
    private readonly DataContext _dataContext;

    public TagService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<Tag>> GetAll()
    {
        return await _dataContext.Tags.ToListAsync();
    }

    public async Task<Tag> GetTagById(int id)
    {
        try
        {
            var tag = await _dataContext.Tags.FindAsync(id);
            if (tag == null)
            {
                throw new NullReferenceException();
            }

            return tag;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Tag CreateTag(Tag input)
    {
        throw new NotImplementedException();
    }

    public Tag UpdateTag(Tag tag)
    {
        throw new NotImplementedException();
    }

    public Tag DeleteTag(int id)
    {
        throw new NotImplementedException();
    }
}