using Microsoft.EntityFrameworkCore;
using simpli.Domain.Entities;

public class NotificationRepo : INotification
{
    private readonly AppDbContext _context;
    private readonly NotificationMappers _mapper;
    public NotificationRepo(AppDbContext context, NotificationMappers mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task ClearAllNotifications(int companyId)
    {
        await _context.Notifications
        .Where(x => x.CompanyId == companyId)
       .ExecuteDeleteAsync();
    }

    public async Task<List<NotificationMessageDto>> GetAllNotifications(int companyID)
    {
        return await _mapper.ProjectToDto(
            _context.Notifications.AsNoTracking()
            .Where(x => x.CompanyId == companyID))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkAllRead(int companyId)
    {
        await _context.Notifications
        .Where(x => x.CompanyId == companyId)
        .ExecuteUpdateAsync(setters => setters.SetProperty(r => r.IsRead, true));
    }
}