using System.Data;
using System.Data.SqlClient;
using TasksAccountingWebAPI.DAL.Entities;
using TasksAccountingWebAPI.DAL.Settings;

namespace TasksAccountingWebAPI.DAL.Repository
{
    public class Repository : IRepository
    {
        private readonly string connectionString; 

        public Repository(ISqlSettings settings)
        {
            connectionString = settings.ConnectionString;
        }

        public async Task<IEnumerable<Report>> GetReportByDateAsync(DateTime date)
        {
            List<Report> reports = new List<Report>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetReportByDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var report = new Report()
                        {
                            SecondName = reader["SecondName"] as string,
                            FirstName = reader["FirstName"] as string,
                            Patronymic = reader["Patronymic"] as string,
                            JobTitle = reader["JobTitle"] as string,
                            Status = reader["Status"] as string,
                            GradeOverall = reader["GradeOverall"] as int? ?? default(int)
                        };
                        reports.Add(report);
                    }
                }
            }
            return await Task.FromResult(reports);
        }

        public async Task<IEnumerable<ApplicantDate>> GetApplicantDates()
        {
            List<ApplicantDate> dates = new List<ApplicantDate>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetApplicantsDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var applicantDates = new ApplicantDate()
                        {
                            ApplicantId = reader["ApplicantId"] as int? ?? default(int),
                            Date = reader["Time"] as DateTime? ?? default(DateTime)
                        };

                        dates.Add(applicantDates);
                    }
                }
            }

            return await Task.FromResult(dates);
        }

        public async Task AddNewApplicantAsync(Applicant applicant)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddNewApplicant", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SecondName", applicant.SecondName);
                cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                cmd.Parameters.AddWithValue("@Patronymic", applicant.Patronymic);
                cmd.Parameters.AddWithValue("@PhoneNumber", applicant.PhoneNumber);
                cmd.Parameters.AddWithValue("@JobTitle", applicant.JobTitle);
                cmd.Parameters.AddWithValue("@DateFirstInterview", applicant.DateFirstInterview);
                cmd.Parameters.AddWithValue("@InterviewerSecondName", applicant.InterviewerSecondName);
                cmd.Parameters.AddWithValue("@InterviewerJobTitle", applicant.InterviewerJobTitle);
                cmd.Parameters.AddWithValue("@Time", applicant.Time);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }

        public async Task DeleteApplicantAsync(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteApplicant", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }

        public async Task UpdateGradeWorksAsync(int id, int grade, string secondName)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateGradeWorks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.Parameters.AddWithValue("@InterviewerSecondName", secondName);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }

        public async Task UpdateDateWorksAsync(int id, DateTime date)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateDateWorks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }
    }
}
