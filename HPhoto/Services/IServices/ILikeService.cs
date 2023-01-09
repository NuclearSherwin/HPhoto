using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services.IServices;

public interface ILikeService
{
    public Task<ActionResult<int>> SaveLikeCount(int likes);
}