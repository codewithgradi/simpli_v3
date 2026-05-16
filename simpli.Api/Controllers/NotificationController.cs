using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simpli.Application.Dtos;
using simpli.Application.Services;

namespace simpli.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly NotificationService _notiService;
    private readonly NotificationMappers _mapper;
    public NotificationController(NotificationService service, NotificationMappers mapper)
    {
      _notiService = service;
      _mapper = mapper;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllNotification()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session");
      var notifcations = await _notiService.GetAllNotifications(companyId);
      if (notifcations == null) return null;
      return Ok(notifcations);
    }
    [Authorize]
    [HttpPatch("mark-read")]
    public async Task<IActionResult> MarkAllRead()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session.");
      await _notiService.MarkAllRead(companyId);
      return NoContent();
    }
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> clearAll()
    {
      var companyID = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyID == null) return Unauthorized();
      await _notiService.ClearAllNotifications(companyID);
      return NoContent();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateNotification(CreateNotificationDto notificationDto)
    {
      var companyID = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyID == null) return Unauthorized();
      notificationDto.CompanyId = companyID;
      var notisf = await _notiService.CreateNotification(notificationDto);
      if (notisf == null) return BadRequest("Could not create Notification");
      return CreatedAtRoute(
        nameof(GetOne),
        new { Id = notisf.Id },
         notisf);

    }
    [Authorize]
    [HttpGet("{id:int}", Name = "GetOne")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
      var notifcation = await _notiService.GetNotification(id);
      if (notifcation == null) return BadRequest("Could not get notification");
      return Ok(notifcation);
    }
  }
}