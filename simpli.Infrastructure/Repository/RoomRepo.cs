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
    public async Task<RoomDto> CreateRoom(CreateRoomDto dto, int companyId)
    {
        var room = _mapper.MapFromCreate(dto);
        room.CompanyId = companyId;
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return _mapper.MapToDto(room);
    }

    public async Task<List<RoomDto>> GetAllRooms(int companyId)
    {
        return await _mapper.ProjectToRoomDto(
            _context.Rooms.AsNoTracking().Where(x => x.CompanyId == companyId)
            )
        .ToListAsync();
    }

    public async Task<RoomDto> GetRoom(int roomId)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
        if (room == null) return null;
        return _mapper.MapToDto(room);

    }

    public async Task<bool> RoomExists(int companyId, int roomId)
    {
        return await _context.Rooms.AnyAsync(x => x.Id == roomId && x.CompanyId == companyId);
    }

    public async Task<RoomDto> UpdateRoom(UpdateRoomDto dto, int roomId, int companyId)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
        if (room == null) return null;

        room.Floor = dto.Floor;
        room.RoomNumber = dto.RoomNumber;
        room.Type = dto.Type;
        room.Status = dto.Status;

        await _context.SaveChangesAsync();
        return _mapper.MapToDto(room);
    }
}