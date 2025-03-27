using System;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please enter your name")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter your last name")]
    [Display(Name = "Last Name")]
    public string Lastname { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "Please enter your email")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Please choose a username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please choose a password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}
