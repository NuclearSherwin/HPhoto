using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPhoto.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
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



        [ForeignKey("TagId")] public Tag Tag { get; set; }
        [ForeignKey("UserId")] public ApplicationUser User { get; set; }

    }
}
