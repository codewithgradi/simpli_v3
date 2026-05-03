

using Microsoft.EntityFrameworkCore;
using simpli.Application;
using simpli.Domain.Entities;

public class CompanyRepo : ICompanyRepo
{
    private AppDbContext _context;
    public CompanyRepo(AppDbContext context)
    {
        _context = context;
    }
    public Task<bool> CompanyExists(int companyId)
    {
        return _context.Companies.AnyAsync(x => x.Id == companyId);
    }

    public Task<CompanyDto> GetCompanyProfile(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<List<VisitorDto>> GetMyVisitors(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task ReactivateProfile(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteCompanyProfile(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyDto> UpdateCompanyProfile(UpdateCompanyProfileDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateExistingCompanyPassword(UpdateCompanyPasswordDto dto)
    {
        throw new NotImplementedException();
    }
}