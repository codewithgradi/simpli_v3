namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class CreateCompanyDto
{
  [Required(ErrorMessage = "Company name is required")]
  public string CompanyName { get; set; }
  [Required(ErrorMessage = "Registration Number is required")]
  public string RegistrationNumber { get; set; }
  [Required(ErrorMessage = "contact number is required")]
  public string ContactNumber { get; set; }
  [Required(ErrorMessage = "Website is required")]
  public string Website { get; set; }

}