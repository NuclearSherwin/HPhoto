using System.ComponentModel.DataAnnotations;

namespace HPhoto
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PhoneNum { get; set; }
        [Required]
        public string FavoriteTopic { get; set; }
        [Required]
        public string ProfileImg { get; set; }

    }
}
