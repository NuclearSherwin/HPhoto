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

    public Task<Comment> Create(Comment input)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> Update(Comment post)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}