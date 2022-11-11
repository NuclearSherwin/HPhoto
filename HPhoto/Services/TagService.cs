using HPhoto.Data;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services;

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

    public async Task<Tag> CreateTag(Tag input)
    {
        try
        {
            _dataContext.Tags.Add(input);
            await _dataContext.SaveChangesAsync();
            return input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Tag> UpdateTag(Tag tag)
    {
        try
        {
            _dataContext.Tags.Update(tag);
            await _dataContext.SaveChangesAsync();
            return tag;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteTag(int id)
    {
        try
        {
            var tag = await _dataContext.Tags.FindAsync(id);
            if (tag == null)
            {
                Console.WriteLine("Tag not found");
                return false;
            }

            _dataContext.Tags.Remove(tag);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}