using System.Data.SqlClient;

namespace TasksAccountingWebAPI.Services
{
    public interface IObserverService
    {
        void CreateConnection();
        void CloseConnection();
        void OnDependencyChange(object sender,
            SqlNotificationEventArgs e);
    }
}
