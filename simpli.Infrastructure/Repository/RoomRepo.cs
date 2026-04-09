using simpli.Application;
using simpli.Domain.Entities;

public class RoomRepo : IRoomRepo
{
    public Task<RoomDto> CreateRoom(CreateRoomDto dto, int companyId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoom(int rommId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RoomDto>> GetAllRooms(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> GetRoom(int companyId, int roomId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RoomExists(int companyId, int roomId)
    {
        throw new NotImplementedException();
    }

    public Task<RoomDto> UpdateRoom(UpdateRoomDto dto, int roomId, int companyId)
    {
        throw new NotImplementedException();
    }
}