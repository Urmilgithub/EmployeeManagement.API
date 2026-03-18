using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.IRepository
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<DepartmentDTO>> GetDepartmentListAsync();
        public Task<DepartmentDTO?> GetDepartmentByIdAsync(Int64 id);
        public Task<AddDepartmentDTO> AddDepartmentAsync(AddDepartmentDTO addDepartmentDTO);
        public Task<UpdateDepartmentDTO?> UpdateDepartmentByIdAsync(Int64 id, UpdateDepartmentDTO updateDepartmentDTO);
        public Task<DepartmentDTO?> DeleteDepartmentByIdAsync(Int64 id);
    }
}
