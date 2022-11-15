using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HPhoto.Model;

namespace HPhoto.Dtos.PostDto;

public class PostUpsertRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public string ImgPath { get; set; }
    
    public IFormFile? Image { get; set; }

    [Required]
    public int TagId { get; set; }

    [Required]
    public int UserId { get; set; }
    
}