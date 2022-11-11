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

    public Task<Comment> GetById(int id)
    {
        throw new NotImplementedException();
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