namespace simpli.Domain;

class Log
{
  public int Id { get; set; }
  public int AdminId { get; set; }
  public string? TargetId { get; set; }
  public AdminAction? Action { get; set; }
  public DateTime Date { get; set; }
}

class AdminAction
{
  public DateTime Date { get; set; }
  public string? Action { get; set; }
}