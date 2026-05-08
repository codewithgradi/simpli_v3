
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CheckIn([FromBody] ) { }

  }
}