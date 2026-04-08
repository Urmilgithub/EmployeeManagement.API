using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public JobRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }


        public async Task<JobDTO> AddJobAsync(AddJobDTO addJobDTO)
        {
            try
            {
                var job = mapper.Map<Job>(addJobDTO);

                await dbContext.Jobs.AddAsync(job);
                await dbContext.SaveChangesAsync();
                var addedJob = await GetJobByIdAsync(job.JobId);

                return addedJob;
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
                var jobDomain = await dbContext.Jobs
                    .Include(x => x.Department).FirstOrDefaultAsync(x=> x.JobId == id);

                if (jobDomain != null)
                {
                    var response = mapper.Map<JobDTO>(jobDomain); 

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
                var jobDomain = await dbContext.Jobs
                    .Include(x=> x.Department).Select(x => new JobDTO
                {
                    JobId = x.JobId,
                    JobTitle = x.JobTitle,
                    MinSalary = x.MinSalary,
                    MaxSalary = x.MaxSalary,
                    DepartmentName = x.Department != null ? x.Department.DepartmentName : null,

                }).ToListAsync();

                return jobDomain;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JobDTO?> UpdateJobByIdAsync(long id, UpdateJobDTO updateJobDTO)
        {
            try
            {
                var jobDomain = await dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);

                if (jobDomain != null)
                {

                    mapper.Map(updateJobDTO, jobDomain);

                    dbContext.Entry(jobDomain).CurrentValues.SetValues(jobDomain);
                    await dbContext.SaveChangesAsync();

                    var updateJob = await GetJobByIdAsync(id);

                    return updateJob;
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
