using System.ComponentModel.DataAnnotations;

namespace LearnerProfile.app.ViewModels.Auth;

public class RegisterParentViewModel
{
    // -- account details --
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string PasswordHash { get; set; } = "";

    [Required(ErrorMessage = "Please confirm your password.")]
    [DataType(DataType.Password)]
    [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";

    // -- personal profile --
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Contact number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string ContactNumber { get; set; } = "";
}