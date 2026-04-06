using Azure;
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace EmployeeManagement.IRepository.Repository
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<DepartmentDTO> AddDepartmentAsync(AddDepartmentDTO addDepartmentDTO)
        {
            try
            {
                var department = new Department
                {
                    DepartmentName = addDepartmentDTO.DepartmentName,
                    CityId = addDepartmentDTO.CityId,
                };

                await dbContext.Departments.AddAsync(department);
                await dbContext.SaveChangesAsync();

                // Get New Data Added
                var addeddepartment = await GetDepartmentByIdAsync(department.DepartmentId);
                return addeddepartment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DepartmentDTO?> DeleteDepartmentByIdAsync(long id)
        {
            try
            {
                var departmentDomain = await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
                if (departmentDomain != null)
                {
                    var department = await GetDepartmentByIdAsync(id);
                    dbContext.Departments.Remove(departmentDomain);
                    return department;
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

        public async Task<DepartmentDTO?> GetDepartmentByIdAsync(long id)
        {
            try
            {
                var departmentDomain = await dbContext.Departments.FindAsync(id);
                if (departmentDomain != null)
                {
                    var response = new DepartmentDTO
                    {
                        DepartmentId = departmentDomain.DepartmentId,
                        DepartmentName = departmentDomain.DepartmentName,
                        CityId = departmentDomain.CityId
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

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentListAsync()
        {
            try
            {
                var departmentDomain = await dbContext.Departments.Select(x => new DepartmentDTO
                {
                    DepartmentName = x.DepartmentName,
                    CityId = x.CityId

                }).ToListAsync();

                return departmentDomain;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DepartmentDTO?> UpdateDepartmentByIdAsync(long id, UpdateDepartmentDTO updateDepartmentDTO)
        {
            try
            {
                var departmentDomain = await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
                if (departmentDomain != null)
                {
                    departmentDomain.DepartmentName = updateDepartmentDTO.DepartmentName;
                    departmentDomain.CityId = updateDepartmentDTO.CityId;

                    dbContext.Departments.Update(departmentDomain);
                    await dbContext.SaveChangesAsync();

                    var updatedDepartment = await GetDepartmentByIdAsync(id);
                    return updatedDepartment;
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
