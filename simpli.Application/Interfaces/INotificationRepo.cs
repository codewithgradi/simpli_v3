using simpli.Domain.Entities;

public interface INotification
{
    Task<List<NotificationMessageDto>> GetAllNotifications(int company);
    Task MarkAllRead(int companyId);
    Task ClearAllNotifications(int companyId);
}