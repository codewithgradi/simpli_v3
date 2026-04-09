using simpli.Domain.Entities;

public interface INotification
{
    Task<List<NotificationDto>> GetAllNotifications(int company);
    Task  MarkAllRead(int companyId);
    Task  ClearAllNotifications(int companyId); 
}