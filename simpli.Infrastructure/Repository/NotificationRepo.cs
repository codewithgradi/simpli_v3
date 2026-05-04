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
    public Task ClearAllNotifications(int companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<NotificationMessageDto>> GetAllNotifications(int companyID)
    {
        return await _mapper.ProjectToDto(
            _context.Notifications.AsNoTracking()
            .Where(x => x.CompanyId == companyID))
            .ToListAsync();
    }

    public Task MarkAllRead(int companyId)
    {
        throw new NotImplementedException();
    }
}