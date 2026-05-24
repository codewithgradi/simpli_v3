namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class CreateCompanyDto
{
  [Required]
  public string? CompanyName { get; set; }
  [Required]
  public string? RegistrationNumber { get; set; }
  [Required]
  public string? ContactNumber { get; set; }
  [Required]
  public string? Website { get; set; }

}