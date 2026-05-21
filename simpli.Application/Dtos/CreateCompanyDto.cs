namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class CreateCompanyDto
{
  [Required]
  public string? CompanyName { get; set; }
  public string? RegistrationNumber { get; set; }
  [Required]
  public string? ContactNumber { get; set; }

  public bool isProfileComplete { get; set; }
  public bool isDeleted { get; set; }
  public string? Website { get; set; }

}