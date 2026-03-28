using EmployeeManagement.IRepository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.WebRequestMethods;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IStateRepository stateRepository;
        private readonly ICityRepository cityRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IJobRepository jobRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository, IStateRepository _stateRepository,
                                    ICityRepository _cityRepository, IDepartmentRepository _departmentRepository,
                                    IJobRepository _jobRepository)
        {
            employeeRepository = _employeeRepository;
            stateRepository = _stateRepository;
            cityRepository = _cityRepository;
            departmentRepository = _departmentRepository;
            jobRepository = _jobRepository;
        }

        [HttpGet("GetEmployeeList")]
        //[Authorize(Roles = "Employee, Manager, Admin")]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeList([FromQuery] string? name,
                                                                    [FromQuery] string? state,
                                                                    [FromQuery] string? department,
                                                                    [FromQuery] string? job,
                                                                    [FromQuery] string? city) 
        {
            var employees = await employeeRepository.GetEmployeeListAsync(name, state, department, job, city);
            return employees;
        }


        [HttpGet("GetEmployeeById")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> GetEmployeesById(Int64 id)
        {
            var employee = await employeeRepository.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }


        [HttpPost("AddEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            var employee = await employeeRepository.AddEmployeeAsync(addEmployeeDTO);
            return Ok(employee);
        }


        [HttpPut("UpdateEmployee")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> UpdateEmployee(Int64 id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            var employee = await employeeRepository.UpdateEmployeeByIdAsync(id, updateEmployeeDTO);
            return Ok(employee);
        }


        [HttpDelete("DeleteEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(Int64 id)
        {
            var employee = await employeeRepository.DeleteEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpGet("GetStateList")]
        public async Task<ActionResult<List<SelectListItem>>> GetStatesAsync()
        {
            var states = await stateRepository.GetStateListAsync(); // Fetch from DB via repo

            var result = states.Select(s => new SelectListItem
            {
                Value = s.StateId.ToString(),
                Text = s.StateName
            }).ToList();

            return Ok(result);
        }


        //public Task<List<SelectItem>> GetCitiesByStateAsync(int stateId)
        //    => http.GetFromJsonAsync<List<SelectItem>>(
        //           $"api/cities/bystate/{stateId}")
        //       ?? Task.FromResult(new List<SelectItem>());

        //public Task<List<SelectItem>> GetDepartmentsAsync()
        //    => http.GetFromJsonAsync<List<SelectItem>>("api/departments")
        //       ?? Task.FromResult(new List<SelectItem>());
    }

}
