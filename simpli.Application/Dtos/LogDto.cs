namespace simpli.Domain.Entities;

public class LogDto
{
  public int Id { get; set; }
  public int AdminId { get; set; }
  public string? TargetId { get; set; }
  public AdminAction? Action { get; set; }
  public DateTime Date { get; set; }
}