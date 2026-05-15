
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using simpli.Application.Dtos;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class VisitorController : ControllerBase
  {
    private readonly IVisitorRepo _visitorRepo;
    private readonly IRoomRepo _roomRepo;
    private readonly VisitorMappers _mapper;
    public VisitorController(IVisitorRepo visitorRepo, VisitorMappers mapper)
    {
      _visitorRepo = visitorRepo;
      _mapper = mapper;
    }
    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDto inDto, [FromBody] string roomNo)
    {
      var companyIdString = User.FindFirstValue("CompanyId");
      if (int.TryParse(companyIdString, out int companyId))
      {
        var roomId = await _roomRepo.GetRoomIdByRoomNumber(companyId, roomNo);
        if (roomId == null) return BadRequest("Room is missing");
        var model = _mapper.MapToEntityFromCeckIn(inDto);
        await _visitorRepo.CheckIn(inDto, companyId, roomId ?? 0);
        var visitorDto = _mapper.MapToDto(model);
        return CreatedAtRoute(nameof(GetVisitor),
        new { Id = visitorDto.Id },
        visitorDto
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
        await _visitorRepo.CheckOut(outDto);
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
      var visitor = await _visitorRepo.GetVisitor(id);
      if (visitor == null) return null;
      return Ok(visitor);
    }
    [HttpGet]
    public async Task<IActionResult> GettAllVisitors()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return NotFound("User Not found.");
      var visitors = await _visitorRepo.GetAllVisitors(companyId);
      if (visitors == null) return null;
      return Ok(visitors);
    }

  }

}