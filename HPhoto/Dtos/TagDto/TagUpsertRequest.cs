using System.ComponentModel.DataAnnotations;

namespace HPhoto.Dtos.TagDto;

public class TagUpsertRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int Rating { get; set; }
}