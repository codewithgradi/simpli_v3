using simpli.Domain.Entities;

namespace simpli.Application.Services;

public class CompanyService
{
  private readonly ICompanyRepo _companyRepo;
  public CompanyService(ICompanyRepo companyRepo)
  {
    _companyRepo = companyRepo;
  }

}