

using Microsoft.EntityFrameworkCore;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Domain.Entities;

public class CompanyRepo : ICompanyRepo
{
    private AppDbContext _context;
    private CompanyMappers _mapper;
    public CompanyRepo(AppDbContext context, CompanyMappers mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<bool> CompanyExists(int companyId)
    {
        return _context.Companies.AnyAsync(x => x.Id == companyId);
    }

    public async Task<CompanyDto> GetCompanyProfile(int companyId)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return null;
        return _mapper.MapToDtoFromGet(company);
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

    public async Task<CompanyDto> UpdateCompanyProfile(int companyId, UpdateCompanyProfileDto dto)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        if (company == null) return null;
        company.Address = dto.Address;
        company.CompanyName = dto.CompanyName;
        company.ContactNumber = dto.ContactNumber;
        company.RegistrationNumber = dto.RegistrationNumber;
        company.Website = dto.Website;
        company.Address = dto.Address;
        await _context.SaveChangesAsync();
        return _mapper.MapToDto(company);
    }

    public async Task UpdateExistingCompanyPassword(int id, UpdateCompanyPasswordDto dto)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
        if (company == null || dto.CurrentPassword != company.Password) return;
        if (dto.ConfirmedPassword != dto.NewPassword) return;

        company.Password = dto.NewPassword;
        await _context.SaveChangesAsync();

    }
}