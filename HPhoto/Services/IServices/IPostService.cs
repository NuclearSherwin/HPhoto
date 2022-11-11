using HPhoto.Model;

namespace HPhoto.Services.IServices;

public interface IPostService
{
    Task<List<Post>> GetAll();
    Task<Post> GetById(int id);
    Task<Post> Create(Post input);
    Task<Post> Update(Post post);
    Task<bool> Delete(int id);
}