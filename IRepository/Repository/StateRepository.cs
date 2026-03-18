using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StateRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<AddStateDTO> AddStateAsync(AddStateDTO addStateDTO)
        {
            try
            {
                var state = new State
                {
                    StateName = addStateDTO.StateName,
                };

                await dbContext.States.AddAsync(state);
                await dbContext.SaveChangesAsync();
                var addedData = await GetStateByIdAsync(state.StateId);
                return addStateDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<StateDTO?> DeleteStateByIdAsync(long id)
        {
            try
            {
                var stateDomain = await dbContext.States.FirstOrDefaultAsync(x => x.StateId == id);
                if (stateDomain != null)
                {
                    var state = await GetStateByIdAsync(id);
                    dbContext.States.Remove(stateDomain);
                    await dbContext.SaveChangesAsync();
                    return state;
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

        public async Task<StateDTO?> GetStateByIdAsync(long id)
        {
            try
            {
                var stateDomain = await dbContext.States.FirstOrDefaultAsync(x => x.StateId == id);
                if (stateDomain != null)
                {
                    var response = new StateDTO
                    {
                        StateId = stateDomain.StateId,
                        StateName = stateDomain.StateName,
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

        public async Task<IEnumerable<StateDTO>> GetStateListAsync()
        {
            try
            {
                var result = await dbContext.States.Select(x => new StateDTO
                {
                    StateId = x.StateId,
                    StateName = x.StateName,

                }).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<UpdateStateDTO?> UpdateStateByIdAsync(long id, UpdateStateDTO updateStateDTO)
        {
            try
            {
                var stateDomain = await dbContext.States.FirstOrDefaultAsync(x => x.StateId == id);
                if (stateDomain != null)
                {
                    stateDomain.StateName = updateStateDTO.StateName;

                    dbContext.Update(stateDomain);
                    await dbContext.SaveChangesAsync();
                    return updateStateDTO;
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
