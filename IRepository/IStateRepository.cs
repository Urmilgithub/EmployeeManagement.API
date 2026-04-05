using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.IRepository
{
    public interface IStateRepository
    {
        public Task<IEnumerable<StateDTO>> GetStateListAsync();
        public Task<StateDTO?> GetStateByIdAsync(Int64 id);
        public Task<StateDTO> AddStateAsync(AddStateDTO addStateDTO);
        public Task<StateDTO?> UpdateStateByIdAsync(Int64 id, UpdateStateDTO updateStateDTO);
        public Task<StateDTO?> DeleteStateByIdAsync(Int64 id);
    }
}
