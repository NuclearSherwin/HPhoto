using HPhoto.Dtos.PostDto;
using HPhoto.Model;

namespace HPhoto.Services.IServices;

public interface IPostService
{
    Task SavePostImageAsync(PostUpsertRequest postUpsertRequest);
    Task<List<Post>> GetAll();
    Task<Post> GetById(int id);
    Task<PostResponse> CreatePostAsync(PostUpsertRequest postUpsertRequest);
    Task<Post> Update(Post post);
    Task<bool> Delete(int id);
}