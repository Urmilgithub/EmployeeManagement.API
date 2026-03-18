using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.IRepository
{
    public interface IJobRepository
    {
        public Task<IEnumerable<JobDTO>> GetJobListAsync();
        public Task<JobDTO?> GetJobByIdAsync(Int64 id);
        public Task<AddJobDTO> AddJobAsync(AddJobDTO addJobDTO);
        public Task<UpdateJobDTO?> UpdateJobByIdAsync(Int64 id, UpdateJobDTO updateJobDTO);
        public Task<JobDTO?> DeleteJobByIdAsync(Int64 id);
    }
}
