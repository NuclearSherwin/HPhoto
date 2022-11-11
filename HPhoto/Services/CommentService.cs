using HPhoto.Data;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services;

public class CommentService : ICommentService
{
    private readonly DataContext _dataContext;

    public CommentService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<Comment>> GetAll()
    {
        return await _dataContext.Comments.ToListAsync();
    }

    public async Task<Comment> GetById(int id)
    {
        try
        {
            var comment = await _dataContext.Comments.FindAsync(id);
            if (comment == null)
            {
                Console.WriteLine("Comment not found");
                throw new NullReferenceException();
            }

            return comment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Comment> Create(Comment input)
    {
        try
        {
            _dataContext.Comments.Add(input);
            await _dataContext.SaveChangesAsync();
            return input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Comment> Update(Comment input)
    {
        try
        {
            _dataContext.Comments.Update(input);
            await _dataContext.SaveChangesAsync();
            return input;
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