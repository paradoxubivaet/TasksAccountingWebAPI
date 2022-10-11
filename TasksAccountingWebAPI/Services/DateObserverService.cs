using TasksAccountingWebAPI.DAL.Entities;
using TasksAccountingWebAPI.DAL.Repository;
using TasksAccountingWebAPI.DAL.Settings;

namespace TasksAccountingWebAPI.Services
{
    // Данный класс представляет собой следящую систему, которая
    // выставляет соискателю оценку 0, если он не успел выполнить
    // задание за данный ему срок.
    public class DateObserverService : IHostedService, IDisposable
    {
        private readonly IRepository repository;
        private Timer? timer = null;
        public DateObserverService(ISqlSettings sqlSettings, IRepository rep)
        {
            repository = rep;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(CheckDate, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        private async void CheckDate(object? state)
        {
            List<ApplicantDate> datesList = repository.GetApplicantDates().Result.ToList();

            foreach(var dates in datesList)
            {
                var applicantId = dates.ApplicantId;
                var date = dates.Date;

                if (date == null)
                    continue;

                if (date <= DateTime.Now)
                {
                    await repository.UpdateGradeWorksAsync(applicantId, 0, "");

                    await repository.UpdateStatusAsync(applicantId, "Истекло время выполнения");
                }
            }

            Console.WriteLine(datesList[0].ApplicantId);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
