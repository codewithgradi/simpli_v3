

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

    public async Task UpdateExistingCompanyPassword(int id, UpdateCompanyPasswordDto dto)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
        if (company == null) return;
        if (dto.CurrentPassword != company.Password || dto.ConfirmedPassword != dto.CurrentPassword) return;


    }
}