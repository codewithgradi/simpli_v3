using simpli.Domain.Entities;

public class NotificationRepo : INotification
{
    public Task ClearAllNotifications(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<List<NotificationDto>> GetAllNotifications(int company)
    {
        throw new NotImplementedException();
    }

    public Task MarkAllRead(int companyId)
    {
        throw new NotImplementedException();
    }
}