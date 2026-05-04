using Microsoft.EntityFrameworkCore;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Domain.Entities;

public class RoomRepo : IRoomRepo
{
    private readonly AppDbContext _context;
    private readonly RoomMappers _mapper;
    public RoomRepo(AppDbContext context, RoomMappers mapper)
    {
        _context = context;
        _mapper = mapper;
    }
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

    public async Task<bool> RoomExists(int companyId, int roomId)
    {
        return await _context.Rooms.AnyAsync();
    }

    public Task<RoomDto> UpdateRoom(UpdateRoomDto dto, int roomId, int companyId)
    {
        throw new NotImplementedException();
    }
}