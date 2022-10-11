using System.Data.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using TasksAccountingWebAPI.DAL.Entities;

namespace TasksAccountingWebAPI.Services
{
    public interface IObserverService
    {
        void OnDependencyChange(object sender,
            RecordChangedEventArgs<Applicant> e);
    }
}
