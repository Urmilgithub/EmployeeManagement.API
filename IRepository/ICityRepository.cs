using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.IRepository
{
    public interface ICityRepository
    {
        public Task<IEnumerable<CityDTO>> GetCityListAsync();
        public Task<CityDTO?> GetCityByIdAsync(Int64 id);
        public Task<CityDTO> AddCityAsync(AddCityDTO addCityDTO);
        public Task<CityDTO?> UpdateCityByIdAsync(Int64 id, UpdateCityDTO updateCityDTO);
        public Task<CityDTO?> DeleteCityByIdAsync(Int64 id);
    }
}
