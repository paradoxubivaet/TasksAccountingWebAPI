using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TasksAccountingWebAPI.DAL.Settings;

namespace TasksAccountingWebAPI.Services
{
    public class ObserverService : BackgroundService, IObserverService
    {
        private readonly string connectionString;
        public ObserverService(ISqlSettings sqlSettings) 
        {
            connectionString = sqlSettings.ConnectionString;
            CreateConnection();
        }

        public void CreateConnection()
        {
            SqlDependency.Start(connectionString);
        }

        public void CloseConnection()
        {
            SqlDependency.Stop(connectionString);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(
                    "SELECT Status FROM Applicants", con))
                {
                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += new
                        OnChangeEventHandler(OnDependencyChange);

                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                        Console.WriteLine("Тест");

                }
            }
            return Task.CompletedTask;
        }

        // Здесь должен быть код уведомления внешней системы
        public void OnDependencyChange(object sender,
            SqlNotificationEventArgs e)
        {
            Console.WriteLine(e.Info);
        }
    }
}
