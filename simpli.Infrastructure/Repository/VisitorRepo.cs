using Microsoft.EntityFrameworkCore;
using simpli.Application.Dtos;
using simpli.Domain;

public class VisitorRepo : IVisitorRepo
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;
    private readonly INotificationRepo _notiRepo;

    public VisitorRepo(AppDbContext context, IEmailService service, INotificationRepo notirepo)
    {
        _context = context;
        _emailService = service;
        _notiRepo = notirepo;
    }
    public async Task<Visitor> CheckIn(Visitor visitor, int companyId, int roomId)
    {
        visitor.CompanyId = companyId;
        visitor.RoomID = roomId;
        visitor.PassCode = Utils.GeneratePasscode();

        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
        if (room == null)
        {
            throw new Exception($"Check-in failed: Room with ID {roomId} does not exist.");
        }

        if (room.Status != RoomStatus.Available)
        {
            throw new Exception("Room is not avaliable");
        }

        visitor.CheckInTime = DateTime.Now;
        _context.Visitors.Add(visitor);
        room.Status = RoomStatus.Occupied;
        room.NumberOfTimesBooked++;



        await _emailService.SendVisitorEmailAsync(
            visitor.Email!,
            visitor.FirstName!,
            room.RoomNumber!,
            visitor.PassCode);



        await _context.SaveChangesAsync();
        return visitor;


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

        var notifcation = _notiRepo
        .CreateNotification(
            new Notification
            {
                CompanyId = visitor.CompanyId,
                VisitorName = visitor.FirstName,
                Status = VisitorStatus.CheckedOut
            },
            visitor.CompanyId);
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