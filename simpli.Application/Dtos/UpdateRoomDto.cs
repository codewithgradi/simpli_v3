namespace simpli.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class UpdateRoomDto
{
  public string? RoomNumber { get; set; }
  public int Floor { get; set; }
  public RoomType RoomType { get; set; }
  public RoomStatus Status { get; set; }
}