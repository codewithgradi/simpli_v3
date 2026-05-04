using simpli.Application.Dtos;
using simpli.Domain.Entities;

public class VisitorRepo : IVisitorRepo
{
    private readonly AppDbContext _context;
    private readonly VisitorMappers _mapper;
    public VisitorRepo(AppDbContext context, VisitorMappers mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<VisitorDto> CheckIn(CheckInDto dto, int companyId, int roomId)
    {
        throw new NotImplementedException();
    }

    public async Task CheckOut(CheckOutDto dto, int id)
    {
        var visitor = await _context.Visitors.FindAsync(id);
        if (visitor == null || dto == null) return;

        var room = await _context.Rooms.FindAsync(dto.roomId);
        if (room == null) return;
        if (visitor.PassCode != dto.Passcode) return;

        visitor.Status = VisitorStatus.CheckedOut;
        room.Status = RoomStatus.Available;
    }

    public async Task<VisitorDto> GetVisitor(int id)
    {
        var visitor = await _context.Visitors.FindAsync(id);
        if (visitor == null) return null;
        return _mapper.MapToDto(visitor);
    }
}