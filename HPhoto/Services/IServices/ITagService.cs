using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services.IServices;

public interface ITagService
{
    Task<ActionResult<List<Tag>>> GetAll();
    Task<ActionResult<List<Tag>>> GetTagById(int id);
    Task<ActionResult<List<Tag>>> CreateTag(Tag tag);
    Task<ActionResult<List<Tag>>> UpdateTag(Tag tag);
    Task<ActionResult<List<Tag>>> DeleteTag(int id);
}