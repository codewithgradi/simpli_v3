using simpli.Application;
using simpli.Domain;
using simpli.Domain.Entities;

public interface ICompanyRepo
{
    Task<Company> GetCompanyProfile(int companyId);
    Task<Company> CreateCompany(Company company);
    Task UpdateExistingCompanyPassword(int id, Company company);
    Task<Company> UpdateCompanyProfile(int comapnyId, Company company);
    Task SoftDeleteCompanyProfile(int companyId);
    Task ReactivateProfile(int companyId);
    Task<bool> CompanyExists(int companyId);
}