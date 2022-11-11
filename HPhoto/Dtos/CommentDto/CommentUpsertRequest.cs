using System.ComponentModel.DataAnnotations;

namespace HPhoto.Dtos.CommentDto;

public class CommentUpsertRequest
{
    [Required]
    public string Content { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public int PostId { get; set; }
}