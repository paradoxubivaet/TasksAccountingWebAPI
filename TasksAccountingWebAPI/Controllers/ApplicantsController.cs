using Microsoft.AspNetCore.Mvc;
using TasksAccountingWebAPI.DAL.Entities;
using TasksAccountingWebAPI.DAL.Repository;

namespace TasksAccountingWebAPI.Controllers
{
    [ApiController]
    [Route("applicants")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IRepository repository;

        public ApplicantsController(IRepository rep)
        {
            repository = rep;
        }

        // GET /applicants/{date}
        [HttpGet("{date}")]
        public async Task<IEnumerable<Report>> GetApplicantsAsync(DateTime date)
        {
            IEnumerable<Report> reports = await repository.GetReportByDateAsync(date);

            return reports;
        }

        // POST /appicants
        [HttpPost]
        public async Task<ActionResult<Applicant>> AddApplicantAsync(Applicant applicant)
        {
            if (applicant == null)
                return BadRequest();

            await repository.AddNewApplicantAsync(applicant);
            return NoContent();
        }

        // PUT /applicants/{id}
        [HttpPut("/status/{id}")]
        public async Task<ActionResult> UpdateStatusAsync(int id, string status)
        {
            if (status == null)
                return BadRequest();

            await repository.UpdateStatusAsync(id, status);
            return NoContent();
        }

        // PUT /applicants/{id}
        [HttpPut("/grade/{id}")]
        public async Task<ActionResult> UpdateGradeAsync(int id, int grade, string secondName)
        {
            await repository.UpdateGradeWorksAsync(id, grade, secondName);
            return NoContent();
        }

        // PUT /applicants/{id}
        [HttpPut("/date/{id}")]
        public async Task<ActionResult> UpdateDateAsync(int id, DateTime date)
        {
            await repository.UpdateDateWorksAsync(id, date);
            return NoContent();
        }

        // DELETE /applicants/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplicantAsync(int id)
        {
            await repository.DeleteApplicantAsync(id);
            return NoContent();
        }
    }
}
