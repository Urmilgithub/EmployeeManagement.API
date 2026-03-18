using EmployeeManagement.IRepository;
using EmployeeManagement.IRepository.Repository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }

        [HttpGet("GetDepartmentList")]
        public async Task<IEnumerable<DepartmentDTO>> GetdepartmentList()
        {
            var department = await departmentRepository.GetDepartmentListAsync();
            return department;
        }


        [HttpGet("GetDepartmentById")]
        public async Task<IActionResult> GetdepartmentById(Int64 id)
        {
            var department = await departmentRepository.GetDepartmentByIdAsync(id);
            return Ok(department);
        }


        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(AddDepartmentDTO addDepartmentDTO)
        {
            var department = await departmentRepository.AddDepartmentAsync(addDepartmentDTO);
            return Ok(department);
        }


        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(Int64 id, UpdateDepartmentDTO updateDepartmentDTO)
        {
            var department = await departmentRepository.UpdateDepartmentByIdAsync(id, updateDepartmentDTO);
            return Ok(department);
        }


        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(Int64 id)
        {
            var department = await departmentRepository.DeleteDepartmentByIdAsync(id);
            return Ok(department);
        }

    }
}
