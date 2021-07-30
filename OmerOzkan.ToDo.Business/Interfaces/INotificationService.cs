using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface INotificationService
    {
        List<Notification> GetNotReadUsers(string appUserId);
        int GetNotReadCountByAppUserId(string AppUserId);
    }
}
