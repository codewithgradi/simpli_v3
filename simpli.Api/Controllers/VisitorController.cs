
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class VisitorController : ControllerBase
  {
    private readonly IVisitorRepo _visitorRepo;
    private readonly IRoomRepo _roomRepo;
    public VisitorController(IVisitorRepo visitorRepo)
    {
      _visitorRepo = visitorRepo;
    }
    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDto inDto, [FromBody] string roomNo)
    {
      var companyIdString = User.FindFirstValue("CompanyId");
      if (int.TryParse(companyIdString, out int companyId))
      {
        var roomId = await _roomRepo.GetRoomIdByRoomNumber(companyId, roomNo);
        if (roomId == null) return BadRequest("Room is missing");
        await _visitorRepo.CheckIn(inDto, companyId, roomId ?? 0);
        return Ok("Check in was a success!");
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

  }
}