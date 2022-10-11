using System.Text;
using System.Text.Json;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;
using TasksAccountingWebAPI.DAL.Entities;
using TasksAccountingWebAPI.DAL.Settings;

namespace TasksAccountingWebAPI.Services
{
    // Данный класс представляет собой следящую систему,
    // которая реагирует на изменения в БД в таблице Applicants
    public class StatusObserverService : BackgroundService
    {
        private readonly string connectionString;
        public StatusObserverService(ISqlSettings sqlSettings) 
        {
            connectionString = sqlSettings.ConnectionString;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                var mapper = new ModelToTableMapper<Applicant>();
                mapper.AddMapping(c => c.Id, "ApplicantId");
                mapper.AddMapping(c => c.SecondName, "SecondName");
                mapper.AddMapping(c => c.FirstName, "FirstName");
                mapper.AddMapping(c => c.Patronymic, "Patronymic");
                mapper.AddMapping(c => c.PhoneNumber, "PhoneNumber");
                mapper.AddMapping(c => c.JobTitle, "JobTitle");
                mapper.AddMapping(c => c.DateFirstInterview, "DateFirstInterview");
                mapper.AddMapping(c => c.InterviewerSecondName, "InterviewerSecondName");
                mapper.AddMapping(c => c.InterviewerJobTitle, "InterviewerJobTitle");
                mapper.AddMapping(c => c.Time, "Time");
                mapper.AddMapping(c => c.Status, "Status");

                using (var dep = new SqlTableDependency<Applicant>(connectionString,
                    "Applicants", mapper: mapper))
                {
                    dep.OnChanged += OnDependencyChange;
                    dep.Start();

                    Console.WriteLine("Чтобы прекратить отслеживать изменений статусов," +
                        " нажмите любую клавишу");
                    Console.ReadKey();

                    dep.Stop();
                }
            });
        }

        // Тут должен быть код отправки на внешнюю систему.
        // Закомментированный код метода PostAsync пример того,
        // какой бы тут должен был быть код.
        public void OnDependencyChange(object sender,
            RecordChangedEventArgs<Applicant> e)
        {
            var changedEntity = e.Entity;

            Console.WriteLine(changedEntity.FirstName + " " + changedEntity.SecondName + 
                ". Статус: " + changedEntity.Status);
        }

        //public async Task PostAsync(Applicant applicant)
        //{

        //    HttpClient client = new HttpClient()
        //    {
        //        BaseAddress = new Uri("https://address.com")
        //    };


        //    using StringContent JsonContent = new(
        //        JsonSerializer.Serialize(new
        //        {
        //            FirstName = applicant.FirstName,
        //            SecondName = applicant.SecondName,
        //            Status = applicant.Status
        //        }),
        //        Encoding.UTF8,
        //        "application/json");

        //    using HttpResponseMessage response = 
        //        await client.PostAsync("applicants", JsonContent);
        //}
    }
}
