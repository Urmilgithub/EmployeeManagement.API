using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext dbContext;

        public JobRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<AddJobDTO> AddJobAsync(AddJobDTO addJobDTO)
        {
            try
            {
                var job = new Job
                {
                    JobTitle = addJobDTO.JobTitle,
                    MinSalary = addJobDTO.MinSalary,
                    MaxSalary = addJobDTO.MaxSalary,
                    DepartmentId = addJobDTO.DepartmentId,
                };

                await dbContext.Jobs.AddAsync(job);
                await dbContext.SaveChangesAsync();
                var addedData = await GetJobByIdAsync(job.JobId);
                return addJobDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JobDTO?> DeleteJobByIdAsync(long id)
        {
            try
            {
                var jobDomain = await dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
                if (jobDomain != null)
                {
                    var job = await GetJobByIdAsync(id);
                    dbContext.Jobs.Remove(jobDomain);
                    await dbContext.SaveChangesAsync();
                    return job;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JobDTO?> GetJobByIdAsync(long id)
        {
            try
            {
                var jobDomain = await dbContext.Jobs.FindAsync(id);
                if (jobDomain != null)
                {
                    var response = new JobDTO
                    {
                        JobId = jobDomain.JobId,
                        JobTitle =jobDomain.JobTitle,
                        MinSalary =jobDomain.MinSalary,
                        MaxSalary =jobDomain.MaxSalary,
                        DepartmentId =jobDomain.DepartmentId,
                    };

                    return response;
                }
                else 
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<JobDTO>> GetJobListAsync()
        {
            try
            {
                var jobDomain = await dbContext.Jobs.Select(x => new JobDTO
                {
                    JobId = x.JobId,
                    JobTitle = x.JobTitle,
                    MinSalary = x.MinSalary,
                    MaxSalary = x.MaxSalary,
                    DepartmentId = x.DepartmentId,

                }).ToListAsync();

                return jobDomain;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UpdateJobDTO?> UpdateJobByIdAsync(long id, UpdateJobDTO updateJobDTO)
        {
            try
            {
                var jobDomain = await dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
                if (jobDomain != null)
                {
                    jobDomain.JobTitle = updateJobDTO.JobTitle;
                    jobDomain.MinSalary = updateJobDTO.MinSalary;
                    jobDomain.MaxSalary = updateJobDTO.MaxSalary;
                    jobDomain.DepartmentId = updateJobDTO.DepartmentId;

                    dbContext.Entry(jobDomain).CurrentValues.SetValues(jobDomain);
                    await dbContext.SaveChangesAsync();
                    var updatedData = await GetJobByIdAsync(id);
                    return updateJobDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
