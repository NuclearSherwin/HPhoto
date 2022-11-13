using System.ComponentModel.DataAnnotations;

namespace HPhoto.Dtos.UserDto;

public class AuthenticationRequest
{
    [Required]
    public string Username { get; set; } = String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
}