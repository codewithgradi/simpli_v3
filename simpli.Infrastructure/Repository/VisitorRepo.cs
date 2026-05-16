using Microsoft.EntityFrameworkCore;
using simpli.Application.Dtos;
using simpli.Domain;
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
    public async Task CheckIn(Visitor entity, int companyId, int roomId)
    {
        var visitor = _mapper.MapToEntityFromCeckIn(entity);
        if (companyId == null || roomId == null) return;
        visitor.CompanyId = companyId;
        visitor.RoomID = roomId;
        visitor.PassCode = Utils.GeneratePasscode();

        _context.Visitors.Add(visitor);
        await _context.SaveChangesAsync();
    }

    public async Task CheckOut(Visitor checkout)
    {
        var visitor = await _context.Visitors.FirstOrDefaultAsync(x => x.PassCode == checkout.PassCode);
        if (visitor == null || checkout == null) return;

        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == checkout.RoomID);
        if (room == null) return;
        if (visitor.PassCode != checkout.PassCode) return;

        visitor.Status = VisitorStatus.CheckedOut;
        visitor.CheckOutTime = DateTime.Now;
        room.Status = RoomStatus.Available;
    }

    public async Task<List<Visitor>> GetAllVisitors(int companyID)
    {
        return await _context.Visitors.AsNoTracking()
        .Where(x => x.CompanyId == companyID)
        .ToListAsync();
    }

    public async Task<Visitor> GetVisitor(int id)
    {
        var visitor = await _context.Visitors.FirstOrDefaultAsync(x => x.Id == id);
        if (visitor == null) return null;
        return visitor;
    }
}