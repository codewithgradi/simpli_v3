using simpli.Application;
using simpli.Domain.Entities;

public interface IRoomRepo
{
    Task<RoomDto> CreateRoom(CreateRoomDto dto, int companyId);
    Task<RoomDto> UpdateRoom(UpdateRoomDto dto, int roomId, int companyId);
    Task<List<RoomDto>> GetAllRooms(int companyId);
    Task<RoomDto> GetRoom(int roomId);
    Task<bool> RoomExists(int companyId, int roomId);

}