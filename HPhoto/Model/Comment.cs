using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPhoto.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int PostId { get; set; }

        [ForeignKey("PostId")] public Post Post { get; set; }

    }
}
