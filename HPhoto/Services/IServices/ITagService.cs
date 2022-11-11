using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services.IServices;

public interface ITagService
{
    Task<List<Tag>> GetAll();
    Tag GetTagById(int id);
    Tag CreateTag(Tag input);
    Tag UpdateTag(Tag tag);
    Tag DeleteTag(int id);
}