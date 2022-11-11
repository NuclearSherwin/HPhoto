using HPhoto.Model;

namespace HPhoto.Services.IServices;

public interface ICommentService
{
    Task<List<Comment>> GetAll();
    Task<Comment> GetById(int id);
    Task<Comment> Create(Comment input);
    Task<Comment> Update(Comment input);
    Task<bool> Delete(int id);
}