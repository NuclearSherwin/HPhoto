using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPhoto.Model;

public class CheckPost
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int TagId { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;


    public string? ImgPath { get; set; }
    
    [Required]
    public DateTime CreatedDate { get; set; }

        
    public bool Published { get; set; }






    [ForeignKey("TagId")] public Tag Tag { get; set; }
    [ForeignKey("UserId")] public User User { get; set; }
}