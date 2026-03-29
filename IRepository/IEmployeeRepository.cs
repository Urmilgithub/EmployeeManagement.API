using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.IRepository
{
    public interface IEmployeeRepository
    {
        public Task<PaginatedResultDTO<EmployeeDTO>> GetEmployeeListAsync(string? name, string? state,
                                                                   string? department, string? job,
                                                                   string? city, string? sortOrder, 
                                                                   string? sortBy, int page = 1);
        public Task<EmployeeDTO?> GetEmployeeByIdAsync(Int64 id);
        public Task<AddEmployeeDTO> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO);
        public Task<UpdateEmployeeDTO?> UpdateEmployeeByIdAsync(Int64 id, UpdateEmployeeDTO updateEmployeeDTO);
        public Task<EmployeeDTO?> DeleteEmployeeByIdAsync(Int64 id);
    }
}
