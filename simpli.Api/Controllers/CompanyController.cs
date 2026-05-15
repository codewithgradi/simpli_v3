using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompanyController : ControllerBase
  {
    private readonly ICompanyRepo _companyRepo;

    public CompanyController(ICompanyRepo companyRepo)
    {
      _companyRepo = companyRepo;
    }

    [Authorize]
    [HttpGet]
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
      var en
      return CreatedAtRoute(
        nameof(GetCompany), new { Id = cre }


      )
    }


  }
}