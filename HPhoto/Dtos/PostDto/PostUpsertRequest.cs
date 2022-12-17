using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HPhoto.Model;
using Microsoft.VisualBasic;

namespace HPhoto.Dtos.PostDto;

public class PostUpsertRequest
{
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int TagId { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? ImgPath { get; set; }
    
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    
    

    
}