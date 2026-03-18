using EmployeeManagement.IRepository;
using EmployeeManagement.IRepository.Repository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository stateRepository;

        public StateController(IStateRepository _stateRepository)
        {
            stateRepository = _stateRepository;
        }

        [HttpGet("GetStateList")]
        public async Task<IEnumerable<StateDTO>> GetStateList()
        {
            var State = await stateRepository.GetStateListAsync();
            return State;
        }


        [HttpGet("GetStateById")]
        public async Task<IActionResult> GetStateById(Int64 id)
        {
            var State = await stateRepository.GetStateByIdAsync(id);
            return Ok(State);
        }


        [HttpPost("AddState")]
        public async Task<IActionResult> AddState(AddStateDTO addStateDTO)
        {
            var State = await stateRepository.AddStateAsync(addStateDTO);
            return Ok(State);
        }


        [HttpPut("UpdateState")]
        public async Task<IActionResult> UpdateState(Int64 id, UpdateStateDTO updateStateDTO)
        {
            var State = await stateRepository.UpdateStateByIdAsync(id, updateStateDTO);
            return Ok(State);
        }


        [HttpDelete("DeleteState")]
        public async Task<IActionResult> DeleteState(Int64 id)
        {
            var State = await stateRepository.DeleteStateByIdAsync(id);
            return Ok(State);
        }
    }
}
