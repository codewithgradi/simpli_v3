using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
{
  public AdditionalUserClaimsPrincipalFactory
  (
    UserManager<AppUser> userManager,
    IOptions<IdentityOptions> options
  ) :
  base(userManager, options)
  { }
  protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser appUser)
  {
    var identity = await base.GenerateClaimsAsync(appUser);
    identity.AddClaim(new Claim("CompanyId", appUser.Company.Id.ToString()));
    return identity;
  }
}