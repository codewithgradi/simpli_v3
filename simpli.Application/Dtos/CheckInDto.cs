namespace simpli.Domain.Entities;

public class CheckInDto
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? IdNumber { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Email { get; set; }
  public DateTime CheckOutTime { get; set; }
  public ReasonForVisit? ReasonForVisit{get;set;}
  public Gender Gender { get; set; }
}