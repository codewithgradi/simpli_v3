using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpli.Application;
using simpli.Domain.Entities;
namespace simpli.Api.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class RoomController : ControllerBase
  {
    private readonly IRoomRepo _roomRepo;
    public RoomController(IRoomRepo roomRepo)
    {
      _roomRepo = roomRepo;
    }
    [Authorize]
    [HttpGet("{companyId:int}")]
    public async Task<IActionResult> GetAllRooms([FromRoute] int companyId)
    {
      var rooms = await _roomRepo.GetAllRooms(companyId);
      if (rooms == null) return BadRequest("Error");
      return Ok(rooms);

    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetRoom([FromQuery] RoomQuery query)
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("No company in session.");
      var room = await _roomRepo.GetRoom(companyId, query.RoomNo);
      if (room == null) return Unauthorized("Missing company id or room number.");
      return Ok(room);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
      [FromBody] CreateRoomDto roomDto)
    {
      try
      {
        var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
        if (companyId == null) return Unauthorized("No company in session");
        var room = await _roomRepo.CreateRoom(roomDto, companyId);
        if (room == null) return BadRequest("Could not create room");
        return Ok(room);
      }
      catch
      {
        return BadRequest("Bad Request!");
      }

    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateRoom(
      [FromBody] UpdateRoomDto updateRoom,
      [FromQuery] UpdateRoomQuery query
    )
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyId"));
      if (companyId == null) return Unauthorized("Invalid session");
      try
      {
        var room = await _roomRepo.UpdateRoom(updateRoom, query.RoomId, query.CompanyID);
        if (room == null) return BadRequest("Could not Update room");
        return NoContent();
      }
      catch
      {
        return BadRequest("Errors updating room");
      }

    }

  }
}