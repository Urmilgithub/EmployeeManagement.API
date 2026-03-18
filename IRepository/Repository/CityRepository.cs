using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CityRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<AddCityDTO> AddCityAsync(AddCityDTO addCityDTO)
        {
            try
            {
                var city = new City
                {
                    CityName = addCityDTO.CityName,
                    StateId = addCityDTO.StateId
                };

                await dbContext.Cities.AddAsync(city);
                await dbContext.SaveChangesAsync();

                // Reload Data for added city 
                var addedcity = await GetCityByIdAsync(city.CityId);
                return addCityDTO;
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
                    var response = new CityDTO
                    {
                        CityId = cityDomain.CityId,
                        CityName = cityDomain.CityName,
                        StateId = cityDomain.StateId
                    };

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
                var cityDomain = await dbContext.Cities.Select(x => new CityDTO
                {
                        CityId = x.CityId,
                        CityName = x.CityName,
                        StateId = x.StateId

                }).ToListAsync();


                return cityDomain;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UpdateCityDTO?> UpdateCityByIdAsync(long id, UpdateCityDTO updateCityDTO)
        {
            try
            {
                var cityDomain = await dbContext.Cities.FindAsync(id);
                if (cityDomain != null)
                {
                    cityDomain.CityName = updateCityDTO.CityName;
                    cityDomain.StateId = updateCityDTO.StateId;

                    dbContext.Update(cityDomain);
                    await dbContext.SaveChangesAsync();

                    // Get Updated Data 
                    var updatedcity = await GetCityByIdAsync(id);

                    return updateCityDTO;
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
