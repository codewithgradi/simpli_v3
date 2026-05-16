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
  public async Task<CompanyDto> CreateCompany(CreateCompanyDto dto)
  {
    var entity = _mapper.MapToEntityFromCreate(dto);
    var company = await _companyRepo.CreateCompany(entity);
    return _mapper.MapToDto(company);
  }
  public async Task<CompanyDto> UpdateCompanyProfile(int companyId, UpdateCompanyProfileDto dto)
  {
    var company = _mapper.MapToEntityFromUpdate(dto);
    company = await _companyRepo.UpdateCompanyProfile(companyId, company);
    return _mapper.MapToDto(company);
  }
}