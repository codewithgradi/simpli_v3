using simpli.Domain;

public interface INotification
{
    Task<List<Notification>> GetAllNotifications(int companyId);
    Task MarkAllRead(int companyId);
    Task<Notification> GetNotification(int id);
    Task ClearAllNotifications(int companyId);
    Task<Notification> CreateNotification(Notification notification);
}