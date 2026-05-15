namespace simpli.Application.Dtos
{
  public class CreateNotificationDto
  {
    public string? VisitorName { get; set; }
    public VisitorStatus Status { get; set; }
  }
}