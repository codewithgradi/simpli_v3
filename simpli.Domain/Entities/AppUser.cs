
using Microsoft.AspNetCore.Identity;
using simpli.Domain;

public class AppUser : IdentityUser
{
  public Company? Company { get; set; }
}