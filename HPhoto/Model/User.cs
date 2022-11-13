using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HPhoto.Model;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Username { get; set; }= string.Empty;
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
}