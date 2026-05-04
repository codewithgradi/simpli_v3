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

    public async Task<List<RoomDto>> GetAllRooms(int companyId)
    {
        return await _mapper.ProjectToRoomDto(_context.Rooms.AsNoTracking())
        .ToListAsync();
    }

    public async Task<RoomDto> GetRoom(int roomId)
    {
        var room = await _context.Rooms.FindAsync(roomId);
        if (room == null) return null;
        return _mapper.MapToDto(room);

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