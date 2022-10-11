using TasksAccountingWebAPI.DAL.Entities;

namespace TasksAccountingWebAPI.DAL.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Report>> GetReportByDateAsync(DateTime date);
        Task<IEnumerable<ApplicantDate>> GetApplicantDates();
        Task AddNewApplicantAsync(Applicant applicant);
        Task DeleteApplicantAsync(int id);
        Task UpdateDateWorksAsync(int id, DateTime date);
        Task UpdateGradeWorksAsync(int id, int grade, string secondName);
        Task UpdateStatusAsync(int id, string status);
    }
}
