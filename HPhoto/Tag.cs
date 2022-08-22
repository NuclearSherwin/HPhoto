using System.ComponentModel.DataAnnotations;

namespace HPhoto
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;


    }
}
