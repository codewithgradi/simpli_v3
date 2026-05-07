namespace simpli.Domain.Entities;

public class CompanyDto
{
  public int Id { get; set; }
  public string? CompanyName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? RegistrationNumber { get; set; }
  public string? ContactNumber { get; set; }
  public bool isProfileComplete { get; set; }
  public bool isDeleted { get; set; }
  public string? Website { get; set; }

  public List<NotificationDto>? Notifications { get; set; }
  public AddressContent? Address { get; set; }
  public DateTime CreatedAt { get; set; }


  public List<RoomDto>? Rooms { get; set; }
  public List<VisitorDto>? Visitors { get; set; }

}