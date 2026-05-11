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
    [HttpGet("{roomNo:int}")]
    public async Task<IActionResult> GetRoom([FromQuery] RoomQuery query)
    {
      var room = await _roomRepo.GetRoomIdByRoomNumber(query.CompanyId, query.RoomNo);
      if (room == null) return BadRequest("Missing company id or room number.")

    }

    [Authorize]
    [HttpPost("{companyId:int}")]
    public async Task<IActionResult> CreateRoom(
      [FromBody] CreateRoomDto roomDto,
      [FromRoute] int companyId)
    {

    }

    [Authorize]
    [HttpPut("{companyId:int}")]
    public async Task<IActionResult> UpdateRoom(
      [FromBody] UpdateRoomDto updateRoom,
      [FromRoute] int companyId,
      int roomId
    )
    {

    }

  }
}