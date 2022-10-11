namespace TasksAccountingWebAPI.DAL.Entities
{
    public class ApplicantDate
    {
        public int ApplicantId { get; set; }

        // Дата, до которой соискатель должен выполнить заданий.
        public DateTime? Date { get; set; }
    }
}
