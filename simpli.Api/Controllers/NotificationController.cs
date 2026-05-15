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
    [HttpGet("{id:int}")]
    public Task<IActionResult> GetAllNotification([FromRoute] int companyId)
    {

    }
  }
}