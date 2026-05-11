using simpli.Application;
using simpli.Domain.Entities;

public interface ICompanyRepo
{
    Task<CompanyDto> GetCompanyProfile(int companyId);
    Task<CompanyDto> CreateCompany(CreateCompanyDto dto);
    Task UpdateExistingCompanyPassword(int id, UpdateCompanyPasswordDto dto);
    Task<CompanyDto> UpdateCompanyProfile(int comapnyId, UpdateCompanyProfileDto dto);
    Task SoftDeleteCompanyProfile(int companyId);
    Task ReactivateProfile(int companyId);
    Task<bool> CompanyExists(int companyId);
}