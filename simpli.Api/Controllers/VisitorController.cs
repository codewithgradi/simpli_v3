
using Microsoft.AspNetCore.Mvc;
using simpli.Domain.Entities;

namespace simpli.Api.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class VisitorController : ControllerBase
  {
    private readonly IVisitorRepo _visitorRepo;
    public VisitorController(IVisitorRepo visitorRepo)
    {
      _visitorRepo = visitorRepo;
    }
    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDto inDto) { }
    public async Task<IActionResult> CheckOut([FromBody] CheckOutDto outDto) { }

  }
}