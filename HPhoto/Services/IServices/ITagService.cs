using HPhoto.Model;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Services.IServices;

public interface ITagService
{
    IList<Tag> GetAll();
    Tag GetById(int id);
    Tag Create(Tag tagInput);
    Tag Update(Tag tagInput);
    bool Detete(int id);
}