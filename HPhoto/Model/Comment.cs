using System.ComponentModel.DataAnnotations.Schema;

namespace HPhoto.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

    }
}
