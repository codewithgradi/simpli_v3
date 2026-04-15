using Microsoft.AspNetCore.Identity;
using simpli.Domain;
public class AppUser : IdentityUser
{
  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpireTime { get; set; }
  public Company? Company { get; set; }
}