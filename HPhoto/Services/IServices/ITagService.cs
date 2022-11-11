using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services.IServices;

public interface ITagService
{
    Task<List<Tag>> GetAll();
    Task<Tag> GetTagById(int id);
    Task<Tag> CreateTag(Tag input);
    Task<Tag> UpdateTag(Tag tag);
    Tag DeleteTag(int id);
}