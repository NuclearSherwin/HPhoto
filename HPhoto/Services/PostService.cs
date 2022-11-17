using HPhoto.Data;
using HPhoto.Dtos.PostDto;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services;

public class PostService : IPostService
{
    private readonly DataContext _dataContext;
    private readonly IWebHostEnvironment _environment;

    public PostService(DataContext dataContext, IWebHostEnvironment environment)
    {
        _dataContext = dataContext;
        _environment = environment;
    }

    public async Task SavePostImageAsync(PostUpsertRequest postUpsertRequest)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(postUpsertRequest.Image.FileName);
        var uploads = Path.Combine(_environment.WebRootPath, "users", "posts", postUpsertRequest.UserId.ToString());
        var filePath = Path.Combine(uploads, uniqueFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

        await postUpsertRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
        postUpsertRequest.ImgPath = filePath;
        Console.WriteLine("Save ImagePath successfully!");
        return;
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

    // include post image
    public async Task<PostResponse> CreatePostAsync(PostUpsertRequest postUpsertRequest)
    {
        var post = new CheckPost
        {
            UserId = postUpsertRequest.UserId,
            TagId = postUpsertRequest.TagId,
            Description = postUpsertRequest.Description,
            ImgPath = postUpsertRequest.ImgPath,
            CreatedDate = DateTime.Now,
            Published = true
        };

        var postEntry = await _dataContext.CheckPosts.AddAsync(post);
        var saveResponse = await _dataContext.SaveChangesAsync();

        if (saveResponse < 0)
        {
            return new PostResponse 
            { 
                Success = false, 
                Error = "Issue while saving the post", 
                ErrorCode = "CP01" 
            };
        }

        var postEntity = postEntry.Entity;
        var postModel = new Post
        {
            Id = postEntity.Id,
            Description = postEntity.Description,
            CreatedDate = post.CreatedDate,
            ImgPath = Path.Combine(postEntity.ImgPath),
            UserId = postEntity.UserId,
            TagId = postEntity.TagId
        };

        return new PostResponse
        {
            Success = true, 
            Post = postModel
        };
    }

    

    // public async Task<Post> Create(Post input)
    // {
    //     try
    //     {
    //         _dataContext.Posts.Add(input);
    //         await _dataContext.SaveChangesAsync();
    //         return input;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e.Message);
    //         throw;
    //     }
    // }

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

    public async Task<bool> Delete(int id)
    {
        try
        {
            var deletePost = await _dataContext.Posts.FindAsync(id);
            if (deletePost == null)
            {
                Console.WriteLine("Post not found!");
                return false;
            }
            _dataContext.Posts.Remove(deletePost);
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