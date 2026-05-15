using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompanyController : ControllerBase
  {
    private readonly ICompanyRepo _companyRepo;
    private readonly CompanyMappers _mapper;

    public CompanyController(ICompanyRepo companyRepo, CompanyMappers mapper)
    {
      _companyRepo = companyRepo;
      _mapper = mapper;
    }

    [Authorize]
    [HttpGet(Name = "GetCompany")]
    public async Task<IActionResult> GetCompany()
    {
      var comapnyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (comapnyId == null) return Unauthorized("Invalid session");
      var company = await _companyRepo.GetCompanyProfile(comapnyId);
      if (company == null) return NotFound("No company profile found");
      return Ok(company);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCompany(CreateCompanyDto createCompany)
    {
      var company = await _companyRepo.CreateCompany(createCompany);
      if (company == null) return Unauthorized("Invalid session");
      var entity = _mapper.MapToEntityFromCreate(createCompany);
      return CreatedAtRoute(
        nameof(GetCompany),
        new { Id = entity.Id },
        _mapper.MapToDto(entity)
      );
    }

    [Authorize]
    [HttpPatch("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdateCompanyPasswordDto dto)
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("Invalid Session");
      var company = await _companyRepo
      .UpdateExistingCompanyPassword(companyId, dto);
      if (company == null) return BadRequest("Could not update password");
      return NoContent();
    }

    [Authorize]
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateCompanyProfileDto dto)
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("Invalid Session");

      var company = await _companyRepo.UpdateCompanyProfile(companyId, dto);
      if (company == null) return BadRequest("Could not update profile");
      return NoContent();
    }
    [Authorize]
    [HttpPut("soft-delete")]
    public async Task<IActionResult> UpdateProfile()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("Invalid Session");
      await _companyRepo.SoftDeleteCompanyProfile(companyId);
      return NoContent();
    }
    [Authorize]
    [HttpPut("reactivate")]
    public async Task<IActionResult> ReactivateProfile()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("Invalid Session");
      await _companyRepo.ReactivateProfile(companyId);
      return NoContent();
    }





  }
}