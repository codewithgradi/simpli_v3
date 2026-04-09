using simpli.Domain;

public class UpdateCompanyProfileDto
{
  public string? CompanyName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? RegistrationNumber { get; set; }
  public string? ContactNumber { get; set; }
   public AddressContent? Address { get; set; }

}