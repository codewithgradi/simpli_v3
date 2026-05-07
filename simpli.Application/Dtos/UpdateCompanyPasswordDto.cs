using System.ComponentModel.DataAnnotations;

namespace simpli.Application;

public class UpdateCompanyPasswordDto
{
  public string? CurrentPassword { get; set; }

  [MinLength(8, ErrorMessage = "Password should be at least 8 characters long")]
  public string? NewPassword { get; set; }

  [MinLength(8, ErrorMessage = "Password should be at least 8 characters long")]
  public string? ConfirmedPassword { get; set; }
}