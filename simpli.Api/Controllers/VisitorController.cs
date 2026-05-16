
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using simpli.Application.Dtos;
using simpli.Application.Services;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class VisitorController : ControllerBase
  {
    private readonly VisitorService _visitorService;

    public VisitorController(VisitorService service)
    {
      _visitorService = service;

    }
    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn(
      [FromBody] CheckInDto inDto,
      [FromBody] string roomNo,
      [FromQuery] int roomId)
    {
      var companyIdString = User.FindFirstValue("CompanyId");
      if (int.TryParse(companyIdString, out int companyId))
      {
        var visitor = await
        _visitorService
        .CheckIn(inDto, companyId, roomId);
        return CreatedAtRoute(nameof(GetVisitor),
        new { Id = visitor.Id },
        visitor
        );
      }
      else
      {
        return BadRequest("Could not check in");
      }
    }

    [HttpPost("check-out")]
    public async Task<IActionResult> CheckOut([FromBody] CheckOutDto outDto)
    {
      try
      {
        await _visitorService.CheckOut(outDto);
        return Ok("Checked out successfully");
      }
      catch
      {
        return BadRequest("Error accessing DB , Could not check out.");
      }
    }
    [HttpGet("{id:int}", Name = "GetVisitor")]
    public async Task<IActionResult> GetVisitor([FromRoute] int id)
    {
      var visitor = await _visitorService.GetVisitor(id);
      if (visitor == null) return null;
      return Ok(visitor);
    }
    [HttpGet]
    public async Task<IActionResult> GettAllVisitors()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return NotFound("User Not found.");
      var visitors = await _visitorService.GetAllVisitors(companyId);
      if (visitors == null) return null;
      return Ok(visitors);
    }

  }

}