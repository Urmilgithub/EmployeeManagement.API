using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CityRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<CityDTO> AddCityAsync(AddCityDTO addCityDTO)
        {
            try
            {
                var city = mapper.Map<City>(addCityDTO);

                await dbContext.Cities.AddAsync(city);
                await dbContext.SaveChangesAsync();

                // Reload Data for added city 
                var addedcity = await GetCityByIdAsync(city.CityId);

                return addedcity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CityDTO?> GetCityByIdAsync(long id)
        {
            try
            {
                var cityDomain = await dbContext.Cities.FirstOrDefaultAsync(x => x.CityId == id);
                if(cityDomain != null)
                {

                    var response = mapper.Map<CityDTO>(cityDomain);

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

        public async Task<CityDTO?> DeleteCityByIdAsync(long id)
        {
            try
            {
                var cityDomain = await dbContext.Cities.FirstOrDefaultAsync(x => x.CityId == id);
                if (cityDomain != null)
                {
                    // Get City Data Before Deletion
                    var cityDTO = await GetCityByIdAsync(id);

                    mapper.Map<CityDTO>(cityDomain);

                    dbContext.Cities.Remove(cityDomain);
                    await dbContext.SaveChangesAsync();

                    return cityDTO;
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

        public async Task<IEnumerable<CityDTO>> GetCityListAsync()
        {
            try
            {
                var cityDomain = await dbContext.Cities
                    .Include(x => x.State)
                    .Select(x => new CityDTO
                {
                        CityId = x.CityId,
                        CityName = x.CityName,
                        StateName = x.State != null ? x.State.StateName : null,

                }).ToListAsync();

                return mapper.Map<IEnumerable<CityDTO>>(cityDomain);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CityDTO?> UpdateCityByIdAsync(long id, UpdateCityDTO updateCityDTO)
        {
            try
            {
                var cityDomain = await dbContext.Cities.FindAsync(id);
                if (cityDomain != null)
                {

                    mapper.Map(updateCityDTO, cityDomain);

                    dbContext.Update(cityDomain);
                    await dbContext.SaveChangesAsync();

                    // Get Updated Data 
                    var updatedcity = await GetCityByIdAsync(id);

                    return updatedcity;
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
