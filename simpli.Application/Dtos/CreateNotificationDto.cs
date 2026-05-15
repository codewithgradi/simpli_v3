namespace simpli.Application.Dtos
{
  public class CreateNotificationDto
  {
    public int CompanyId { get; set; }
    public string? VisitorName { get; set; }
    public VisitorStatus Status { get; set; }
  }
}