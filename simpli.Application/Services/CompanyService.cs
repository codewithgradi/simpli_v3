using simpli.Application.Dtos;
using simpli.Domain.Entities;

namespace simpli.Application.Services;

public class CompanyService
{
  private readonly ICompanyRepo _companyRepo;
  private readonly CompanyMappers _mapper;

  public CompanyService(ICompanyRepo companyRepo, CompanyMappers mapper)
  {
    _companyRepo = companyRepo;
    _mapper = mapper;
  }
  public async Task<CompanyDto> GetCompanyProfile(int companyId)
  {
    var company = await _companyRepo.GetCompanyProfile(companyId);
    return _mapper.MapToDto(company);
  }

}