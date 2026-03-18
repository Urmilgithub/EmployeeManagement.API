using EmployeeManagement.IRepository;
using EmployeeManagement.IRepository.Repository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;

        public CityController(ICityRepository _cityRepository)
        {
            cityRepository = _cityRepository;
        }

        [HttpGet("GetCityList")]
        public async Task<IEnumerable<CityDTO>> GetCityList()
        {
            var city = await cityRepository.GetCityListAsync();
            return city;
        }


        [HttpGet("GetCityById")]
        public async Task<IActionResult> GetCityById(Int64 id)
        {
            var city = await cityRepository.GetCityByIdAsync(id);
            return Ok(city);
        }


        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity(AddCityDTO addCityDTO)
        {
            var city = await cityRepository.AddCityAsync(addCityDTO);
            return Ok(city);
        }


        [HttpPut("UpdateCity")]
        public async Task<IActionResult> UpdateCity(Int64 id, UpdateCityDTO updateCityDTO)
        {
            var city = await cityRepository.UpdateCityByIdAsync(id, updateCityDTO);
            return Ok(city);
        }


        [HttpDelete("DeleteCity")]
        public async Task<IActionResult> DeleteCity(Int64 id)
        {
            var city = await cityRepository.DeleteCityByIdAsync(id);
            return Ok(city);
        }
    }
}
