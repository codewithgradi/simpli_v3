namespace simpli.Application;

public class CreateRoomDto
{
  public int CompanyId { get; set; }
  public RoomStatus Status { get; set; } = RoomStatus.Available;
  public string? Floor { get; set; }
  public string? RoomNumber { get; set; }
}