

using Microsoft.EntityFrameworkCore;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Domain;
using simpli.Domain.Entities;

public class CompanyRepo : ICompanyRepo
{
    private readonly AppDbContext _context;
    public CompanyRepo(AppDbContext context)
    {
        _context = context;
    }
    public Task<bool> CompanyExists(int companyId)
    {
        return _context.Companies.AnyAsync(x => x.Id == companyId);
    }

    public async Task<Company> GetCompanyProfile(int companyId)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return null;
        return company;
    }

    public async Task<Company> CreateCompany(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task ReactivateProfile(int companyId)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return;
        company.isDeleted = false;
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteCompanyProfile(int companyId)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return;
        company.isDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<Company> UpdateCompanyProfile(int companyId, Company updatedCompany)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return null;
        company.Address = updatedCompany.Address;
        company.CompanyName = updatedCompany.CompanyName;
        company.ContactNumber = updatedCompany.ContactNumber;
        company.RegistrationNumber = updatedCompany.RegistrationNumber;
        company.Website = updatedCompany.Website;
        company.Address = updatedCompany.Address;
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task UpdateExistingCompanyPassword(int id, Company curentComp)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
        company.Password = curentComp.Password;
        await _context.SaveChangesAsync();

    }
}