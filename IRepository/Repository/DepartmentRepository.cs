using AutoMapper;
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
        private readonly IMapper mapper;

        public DepartmentRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }


        public async Task<DepartmentDTO> AddDepartmentAsync(AddDepartmentDTO addDepartmentDTO)
        {
            try
            {

                var department = mapper.Map<Department>(addDepartmentDTO);

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
                    await dbContext.SaveChangesAsync();

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
                var departmentDomain = await dbContext.Departments
                                    .Include(x => x.City)
                                    .FirstOrDefaultAsync(x => x.DepartmentId == id);

                if (departmentDomain != null)
                {

                    var response = mapper.Map<DepartmentDTO>(departmentDomain);

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
                var departmentDomain = await dbContext.Departments
                    .Include(x => x.City)
                    .Select(x => new DepartmentDTO
                {
                    DepartmentName = x.DepartmentName,
                    CityName = x.City != null ? x.City.CityName : null

                }).ToListAsync();

                return mapper.Map<IEnumerable<DepartmentDTO>>(departmentDomain);
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

                    mapper.Map(updateDepartmentDTO, departmentDomain);

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
