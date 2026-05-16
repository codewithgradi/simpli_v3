using Microsoft.EntityFrameworkCore;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Domain;
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
    public async Task<Room> CreateRoom(Room room, int companyId)
    {
        room.CompanyId = companyId;
        _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<List<Room>> GetAllRooms(int companyId)
    {
        return await
            _context.Rooms.AsNoTracking()
            .Where(x => x.CompanyId == companyId)
        .ToListAsync();
    }
    public async Task<int?> GetRoomIdByRoomNumber(int companyId, string roomNo)
    {
        var room = await _context.Rooms
        .FirstOrDefaultAsync(
            x => x.CompanyId == companyId
        && x.RoomNumber == roomNo
        );
        if (room == null) return null;
        return room.Id;
    }

    public async Task<Room> GetRoom(int companyId, string roomNo)
    {
        var room = await _context.Rooms
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.RoomNumber == roomNo && x.CompanyId == companyId);

        if (room == null) return null;
        return room;

    }

    public async Task<bool> RoomExists(int companyId, int roomId)
    {
        return await _context.Rooms.AnyAsync(x => x.Id == roomId && x.CompanyId == companyId);
    }

    public async Task<Room> UpdateRoom(Room updatedRoom, int roomId, int companyId)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && companyId == x.CompanyId);
        if (room == null) return null;

        room.Floor = updatedRoom.Floor;
        room.RoomNumber = updatedRoom.RoomNumber;
        room.Type = updatedRoom.Type;
        room.Status = updatedRoom.Status;

        await _context.SaveChangesAsync();
        return room;
    }


}