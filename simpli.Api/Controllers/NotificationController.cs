using Microsoft.AspNetCore.Mvc;

namespace simpli.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly INotification _notificationRepo;
    public NotificationController(INotification notification)
    {
      _notificationRepo = notification;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllNotification()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session");
      var notifcations = await _notificationRepo.GetAllNotifications(companyId);
      if (notifcations == null) return null;
      return Ok(notifcations);
    }

    [HttpPatch("mark-read")]
    public async Task<IActionResult> MarkAllRead()
    {
      var companyId = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyId == null) return Unauthorized("Invalid session.");
      await _notificationRepo.MarkAllRead(companyId);
      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> clearAll()
    {
      var companyID = Convert.ToInt32(User.FindFirst("CompanyID"));
      if (companyID == null) return Unauthorized();
      await _notificationRepo.ClearAllNotifications(companyID);
      return NoContent();
    }
  }
}