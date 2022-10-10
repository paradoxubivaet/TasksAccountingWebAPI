namespace TasksAccountingWebAPI.DAL.Entities
{
    public class Applicant
    {
        public int Id { get; set; } 
        public string? SecondName { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }

        // Дата первичного собеседования
        public DateTime DateFirstInterview { get; set; }

        // Фамилия человека, который проводит интервью
        public string? InterviewerSecondName { get; set; }
        public string? InterviewerJobTitle { get; set; }

        // Дата, для окончания выполнения задания.
        public DateTime Time { get; set; }

        // Статус работы 
        public string? Status { get; set; }
    }
}
