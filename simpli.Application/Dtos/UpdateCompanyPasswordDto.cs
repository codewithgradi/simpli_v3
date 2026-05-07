namespace simpli.Application;

public class UpdateCompanyPasswordDto
{
  public string? CurrentPassword { get; set; }
  public string? ConfirmedPassword { get; set; }
}