namespace simpli.Domain.Dtos;

public class RoomDto
{
  public int Id { get; set; }
  public string? RoomNumber { get; set; }
  public int Floor { get; set; }
  public RoomType RoomType { get; set; }
  public RoomStatus Status;
  public int NumberOfTimesBooked { get; set; } = 0;
  public int CompanyId { get; set; }

}