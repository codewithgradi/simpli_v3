using simpli.Domain;

public interface INotificationRepo
{
    Task<List<Notification>> GetAllNotifications(int companyId);
    Task MarkAllRead(int companyId);
    Task<Notification> GetNotification(int id);
    Task ClearAllNotifications(int companyId);
    Task<Notification> CreateNotification(Notification notification);
}