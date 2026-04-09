using simpli.Domain.Entities;

public interface IIdentityService
{
  Task LoginAsync(LogDto dto);
  Task RegisterAsync(CreateCompanyDto dto);
  Task LogoutAsync();
}