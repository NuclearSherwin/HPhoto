using HPhoto.Data;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services;

public class PostService : IPostService
{
    private readonly DataContext _dataContext;

    public PostService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<Post>> GetAll()
    {
        return await _dataContext.Posts.ToListAsync();
    }

    public async Task<Post> GetById(int id)
    {
        try
        {
            var post = await _dataContext.Posts.FindAsync(id);
            if (post == null)
            {
                Console.WriteLine("Post not found");
                throw new NullReferenceException();
            }

            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        
    }

    public async Task<Post> Create(Post input)
    {
        try
        {
            _dataContext.Posts.Add(input);
            await _dataContext.SaveChangesAsync();
            return input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Post> Update(Post post)
    {
        try
        {
            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();
            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}