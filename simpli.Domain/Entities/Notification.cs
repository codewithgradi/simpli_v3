namespace simpli.Domain;

class Notification
{
  public int Id { get; set; }
  public int CompanyId { get; set; }
  public string? VisitorName { get; set; }
  public bool IsRead { get; set; }
  public VisitorStatus Status { get; set; }
  public DateTime CreatedAt { get; set; }
}