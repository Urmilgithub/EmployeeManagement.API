using EmployeeManagement.IRepository;
using EmployeeManagement.IRepository.Repository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }


        [HttpGet("GetJobList")]
        public async Task<IEnumerable<JobDTO>> GetJobList()
        {
            var Job = await jobRepository.GetJobListAsync();
            return Job;
        }


        [HttpGet("GetJobById")]
        public async Task<IActionResult> GetJobById(Int64 id)
        {
            var Job = await jobRepository.GetJobByIdAsync(id);
            return Ok(Job);
        }


        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob(AddJobDTO addJobDTO)
        {
            var Job = await jobRepository.AddJobAsync(addJobDTO);
            return Ok(Job);
        }


        [HttpPut("UpdateJob")]
        public async Task<IActionResult> UpdateJob(Int64 id, UpdateJobDTO updateJobDTO)
        {
            var Job = await jobRepository.UpdateJobByIdAsync(id, updateJobDTO);
            return Ok(Job);
        }


        [HttpDelete("DeleteJob")]
        public async Task<IActionResult> DeleteJob(Int64 id)
        {
            var Job = await jobRepository.DeleteJobByIdAsync(id);
            return Ok(Job);
        }
    }
}
