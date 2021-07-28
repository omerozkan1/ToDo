using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.DataAccess.Interfaces
{
    public interface INotificationDal : IGenericDal<Notification>
    {
        List<Notification> GetNotReadUsers(string appUserId);
        int GetNotReadCountByAppUserId(string appUserId);
    }
}
