using Microsoft.EntityFrameworkCore;
using simpli.Application.Dtos;
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

    public async Task<NotificationDto> CreateNotification(CreateNotificationDto dto)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == dto.CompanyId);
        if (company == null) return null;
        var entity = _mapper.MapToDtoFromCreate(dto);
        var notification = await _context.Notifications.AddAsync(entity);
        if (notification == null) return null;
        await _context.SaveChangesAsync();
        return _mapper.MapToDto(entity);
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