using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<AddEmployeeDTO> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO)
        {
            try
            {
                var employee = new Employee
                {
                    Name = addEmployeeDTO.Name,
                    Email = addEmployeeDTO.Email,
                    Gender = addEmployeeDTO.Gender,
                    Contact = addEmployeeDTO.Contact,
                    Salary = addEmployeeDTO.Salary,
                    JoinDate = addEmployeeDTO.JoinDate,
                    DepartmentId = addEmployeeDTO.DepartmentId,
                    StateId = addEmployeeDTO.StateId,
                    JobId = addEmployeeDTO.JobId,
                    CityId = addEmployeeDTO.CityId
                };

                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();

                var addemployeeDTO = new EmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    Contact = employee.Contact,
                    Salary = employee.Salary,
                    JoinDate = employee.JoinDate,
                    DepartmentId = employee.DepartmentId,
                    StateId = employee.StateId,
                    JobId = employee.JobId,
                    CityId = employee.CityId
                };

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
                    var response = new EmployeeDTO
                    {
                        EmployeeId = employeeDomain.EmployeeId,
                        Name = employeeDomain.Name,
                        Email = employeeDomain.Email,
                        Gender = employeeDomain.Gender,
                        Contact = employeeDomain.Contact,
                        Salary = employeeDomain.Salary,
                        JoinDate = employeeDomain.JoinDate,
                        StateName = employeeDomain.State != null ? employeeDomain.State.StateName : null,
                        CityName = employeeDomain.City.CityName,
                        DepartmentName = employeeDomain.Department.DepartmentName,
                        JobTitle = employeeDomain.Job.JobTitle
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

                    "Salary" => isDesc ? query.OrderByDescending(x => x.Salary) : query.OrderBy(x => x.Salary),

                    "JoinDate" => isDesc ? query.OrderByDescending(x => x.JoinDate) : query.OrderBy(x => x.JoinDate),

                    _ => query // NO sorting of soryBy is null
                };


                // pagination

                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize); 


                var result = await query
                            .Skip((page -1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();


                IEnumerable<EmployeeDTO> res = result.Select(item => new EmployeeDTO
                {
                    EmployeeId = item.EmployeeId,
                    Name = item.Name,
                    Email = item.Email,
                    Gender = item.Gender,
                    Contact = item.Contact,
                    Salary = item.Salary,
                    JoinDate = item.JoinDate,
                    StateName = item.State?.StateName,
                    CityName = item.City?.CityName,
                    DepartmentName = item.Department?.DepartmentName,
                    JobTitle = item.Job?.JobTitle
                }).ToList();

                return new PaginatedResultDTO<EmployeeDTO>
                {
                    Items = res,
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
                var employeeDomain = await dbContext.Employees.FindAsync(id);

                if (employeeDomain != null)
                {
                    employeeDomain.Name = updateEmployeeDTO.Name;
                    employeeDomain.Email = updateEmployeeDTO.Email;
                    employeeDomain.Gender = updateEmployeeDTO.Gender;
                    employeeDomain.Contact = updateEmployeeDTO.Contact;
                    employeeDomain.Salary = updateEmployeeDTO.Salary;
                    employeeDomain.JoinDate = updateEmployeeDTO.JoinDate;
                    employeeDomain.DepartmentId = updateEmployeeDTO.DepartmentId;
                    employeeDomain.StateId = updateEmployeeDTO.StateId;
                    employeeDomain.JobId = updateEmployeeDTO.JobId;
                    employeeDomain.CityId = updateEmployeeDTO.CityId;

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
