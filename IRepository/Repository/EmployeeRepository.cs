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

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeListAsync()
        {    
            try
            {
                var result = await dbContext.Employees.Include(x=> x.State).ToListAsync();
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

                return res;
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
