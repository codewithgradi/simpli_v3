using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpli.Application.Dtos;

namespace simpli.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly INotification _notificationRepo;
    private readonly NotificationMappers _mapper;
    public NotificationController(INotification notification, NotificationMappers mapper)
    {
      _notificationRepo = notification;
      _mapper = mapper;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllNotification()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session");
      var notifcations = await _notificationRepo.GetAllNotifications(companyId);
      if (notifcations == null) return null;
      return Ok(notifcations);
    }
    [Authorize]
    [HttpPatch("mark-read")]
    public async Task<IActionResult> MarkAllRead()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session.");
      await _notificationRepo.MarkAllRead(companyId);
      return NoContent();
    }
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> clearAll()
    {
      var companyID = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyID == null) return Unauthorized();
      await _notificationRepo.ClearAllNotifications(companyID);
      return NoContent();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateNotification(CreateNotificationDto notificationDto)
    {
      var companyID = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyID == null) return Unauthorized();
      notificationDto.CompanyId = companyID;
      var notisf = await _notificationRepo.CreateNotification(notificationDto);
      var entity = _mapper.MapToDtoFromCreate(notificationDto);
      if (notisf == null) return BadRequest("Could not create Notification");
      return CreatedAtRoute(
        nameof(GetOne),
        new { Id = entity.Id },
         _mapper.MapToDto(entity));

    }
    [Authorize]
    [HttpGet("{id:int}", Name = "GetOne")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
      var notifcation = await _notificationRepo.GetNotification(id);
      if (notifcation == null) return BadRequest("Could not get notification");
      return Ok(notifcation);
    }
  }
}