using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeManagement.IRepository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<AddEmployeeDTO> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO)
        {
            try
            {
                var employee = mapper.Map<Employee>(addEmployeeDTO);

                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();


                // Reload with related data
                var addedEmployee = await GetEmployeeByIdAsync(employee.EmployeeId);

                return addEmployeeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<EmployeeDTO?> DeleteEmployeeByIdAsync(long id)
        {
            try
            {
                var employeeDomain = await dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
                if (employeeDomain != null)
                {
                    // Get employee data before deletion
                    var employeeDTO = await GetEmployeeByIdAsync(id);

                    mapper.Map<EmployeeDTO>(employeeDomain);

                    dbContext.Employees.Remove(employeeDomain);
                    await dbContext.SaveChangesAsync();

                    return employeeDTO;
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


        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(long id)
        {
            try
            {
                var employeeDomain = await dbContext.Employees
                    .Include(e => e.Department)
                    .Include(e => e.State)
                    .Include(e => e.City)
                    .Include(e => e.Job)
                    .FirstOrDefaultAsync(x => x.EmployeeId == id);

                if (employeeDomain != null)
                {

                    return mapper.Map<EmployeeDTO>(employeeDomain);
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

        public async Task<PaginatedResultDTO<EmployeeDTO>> GetEmployeeListAsync(string? name, string? state,
                                                                         string? department, string? job,
                                                                         string? city, string? sortOrder,
                                                                         string? sortBy, int page = 1)
        {

            try
            {
                const int pageSize = 10;


                var query = dbContext.Employees
                    .Include(x => x.State)
                    .Include(x => x.City)
                    .Include(x => x.Department)
                    .Include(x => x.Job)
                    .AsQueryable();

                // Apply filters only if values are provided

                if (!String.IsNullOrWhiteSpace(name))
                    query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));

                if (!String.IsNullOrWhiteSpace(state))
                    query = query.Where(x => x.State != null && x.State.StateName.ToLower().Contains(state.ToLower()));

                if (!string.IsNullOrWhiteSpace(department))
                    query = query.Where(x => x.Department != null && x.Department.DepartmentName.ToLower().Contains(department.ToLower()));

                if (!string.IsNullOrWhiteSpace(job))
                    query = query.Where(x => x.Job != null && x.Job.JobTitle.ToLower().Contains(job.ToLower()));

                if (!string.IsNullOrWhiteSpace(city))
                    query = query.Where(x => x.City != null && x.City.CityName.ToLower().Contains(city.ToLower()));



                // Sorting

                bool isDesc = String.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);
                query = sortBy?.ToLower() switch
                {

                    "salary" => isDesc ? query.OrderByDescending(x => x.Salary) : query.OrderBy(x => x.Salary),

                    "joindate" => isDesc ? query.OrderByDescending(x => x.JoinDate) : query.OrderBy(x => x.JoinDate),

                    _ => query // NO sorting of soryBy is null
                };


                // pagination

                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


                var result = await query
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();


                //IEnumerable<EmployeeDTO> res = result.Select(item => new EmployeeDTO
                //{
                //    EmployeeId = item.EmployeeId,
                //    Name = item.Name,
                //    Email = item.Email,
                //    Gender = item.Gender,
                //    Contact = item.Contact,
                //    Salary = item.Salary,
                //    JoinDate = item.JoinDate,
                //    StateName = item.State?.StateName,
                //    CityName = item.City?.CityName,
                //    DepartmentName = item.Department?.DepartmentName,
                //    JobTitle = item.Job?.JobTitle

                //}).ToList(); 


                return new PaginatedResultDTO<EmployeeDTO>
                {
                    Items = mapper.Map<IEnumerable<EmployeeDTO>>(result),
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageNumber = page,
                    PageSize = pageSize,
                };

                // return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UpdateEmployeeDTO?> UpdateEmployeeByIdAsync(long id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            try
            {
                var employeeDomain = await dbContext.Employees
                    .FirstOrDefaultAsync(x => x.EmployeeId == id);

                if (employeeDomain != null)
                {

                    mapper.Map(updateEmployeeDTO, employeeDomain);

                    dbContext.Employees.Update(employeeDomain);
                    await dbContext.SaveChangesAsync();

                    // Return updated employee with related data

                    var updatedEmployee = await GetEmployeeByIdAsync(employeeDomain.EmployeeId);
                    return updateEmployeeDTO;
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
