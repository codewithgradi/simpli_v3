namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class CreateCompanyDto
{
  [Required]
  public string? CompanyName { get; set; }
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
  [Required]
  [MinLength(8, ErrorMessage = "Password should be at least 8 characters long.")]
  public string? Password { get; set; }
  public string? RegistrationNumber { get; set; }
  [Required]
  public string? ContactNumber { get; set; }

  public bool isProfileComplete { get; set; }
  public bool isDeleted { get; set; }
  public string? Website { get; set; }

}