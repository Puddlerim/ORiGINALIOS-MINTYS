using System.ComponentModel.DataAnnotations;

namespace ZaidimuPortalasMVC.Models;

public sealed record FriendViewModel(int Id, string DisplayName, string Username);

public sealed class RemoveFriendViewModel
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Įveskite savo paskyros slaptažodį.")]
    [DataType(DataType.Password)]
    [Display(Name = "Jūsų paskyros slaptažodis")]
    public string Password { get; set; } = string.Empty;
}
