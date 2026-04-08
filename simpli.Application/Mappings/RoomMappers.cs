namespace simpli.Application.Dtos;

using Riok.Mapperly.Abstractions;
using simpli.Domain;
using simpli.Domain.Entities;

[Mapper]
public partial class RoomMappers
{
  public partial RoomDto MapToDto(Room room);
  public partial Room MapToEntity(RoomDto dto);
  public partial RoomDto MapFromCreate(CreateRoomDto dto);
}
