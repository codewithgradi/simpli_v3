namespace simpli.Domain.Entities;

public class NotificationDto
{
  
  public string? VisitorName { get; set; }
  public bool IsRead { get; set; }
  public VisitorStatus Status { get; set; }
  public DateTime CreatedAt { get; set; }

}