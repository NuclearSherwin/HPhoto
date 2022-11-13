using System.ComponentModel.DataAnnotations;

namespace HPhoto.Dtos.UserDto;

public class RegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(3, ErrorMessage = "Please enter at least 3 characters!")]
    public string FirstName { get; set; } = string.Empty;
    [Required, MinLength(3, ErrorMessage = "Please enter at least 3 characters!")]
    public string UserName { get; set; } = string.Empty;
    [Required, MinLength(3, ErrorMessage = "Please enter at least 3 characters!")]
    public string LastName { get; set; } = string.Empty;
    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters!")] 
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")]
    public string PasswordConfirmation { get; set; } = string.Empty;
}