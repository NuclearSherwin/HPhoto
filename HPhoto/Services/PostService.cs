using HPhoto.Data;
using HPhoto.Dtos.PostDto;
using HPhoto.Model;
using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Services;

public class PostService : IPostService
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _environment;

    public PostService(DataContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    // public async Task SavePostImageAsync(PostUpsertRequest postUpsertRequest)
    // {
    //     var uniqueFileName = FileHelper.GetUniqueFileName(postUpsertRequest.Image.FileName);
    //     var uploads = Path.Combine(_environment.WebRootPath, "users", "posts", postUpsertRequest.UserId.ToString());
    //     var filePath = Path.Combine(uploads, uniqueFileName);
    //
    //     Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
    //
    //     await postUpsertRequest.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
    //     postUpsertRequest.ImgPath = filePath;
    //     Console.WriteLine("Save ImagePath successfully!");
    //     return;
    // }

    public async Task<List<Post>> GetAll()
    {
        try
        {
            var results = await _db.Posts.
                ToListAsync();
            return results;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    

    public async Task<Post> GetById(int id)
    {
        try
        {
            var postFromDb = await _db.Posts.FindAsync(id);
            if (postFromDb == null) throw new NullReferenceException();

            return postFromDb;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Post> Create([FromForm]Post input)
    {
        try
        {
            input.CreatedDate = DateTime.Now;
            input.ImgPath = await SaveImgNormal(input.ImageFile);
            _db.Posts.Add(input);
            await _db.SaveChangesAsync();

            return input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    

    public async Task<Post> Update(Post input)
    {
        try
        {
            input.ImgPath = await SaveImg(input.ImageFile);
            _db.Posts.Update(input);
            await _db.SaveChangesAsync();

            return input;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var postToDelete = await _db.Posts.FindAsync(id);
            if (postToDelete == null) return false;

            _db.Posts.Remove(postToDelete);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> SaveImg(IFormFile imgFile)
    {
        var special = Guid.NewGuid().ToString();
        
        string imgName = new string(Path.GetFileNameWithoutExtension(imgFile.FileName).Take(10)
            .ToArray()).Replace(' ', '-');
        imgName = imgName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imgFile.FileName);
        
        
        var imgPath = Path.Combine(_environment.ContentRootPath, "Images", special + "-" + imgName);
        using (var fileStream = new FileStream(imgPath, FileMode.Create))
        {
            await imgFile.CopyToAsync(fileStream);
        }

        return imgName;
    }
    
    public async Task<string> SaveImgNormal(IFormFile imgFile)
    {
        string imageName = new String(Path.GetFileNameWithoutExtension(imgFile.FileName).Take(10).ToArray()).Replace(' ', '-');
        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imgFile.FileName);
        var imagePath = Path.Combine(_environment.ContentRootPath, "Images", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imgFile.CopyToAsync(fileStream);
        }
        return imageName;
    }
}