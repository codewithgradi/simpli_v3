using simpli.Application;
using simpli.Domain.Entities;

public interface ICompanyRepo
{
    Task<List<VisitorDto>> GetMyVisitors(int companyId); 
    Task<CompanyDto> GetCompanyProfile (int companyId);
    Task UpdateExistingCompanyPassword(UpdateCompanyPasswordDto dto);
    Task<CompanyDto> UpdateCompanyProfile(UpdateCompanyProfileDto dto);
    Task SoftDeleteCompanyProfile(int companyId);
    Task ReactivateProfile(int companyId);
    Task<bool> CompanyExists(int companyId);
}