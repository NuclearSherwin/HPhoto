using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services;

public class LikeService : ILikeService
{
    public async Task<ActionResult<int>> SaveLikeCount(int likes)
    {
        // Save the like count to a database or file
        return likes;
    }
}