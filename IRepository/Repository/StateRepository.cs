using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public StateRepository(ApplicationDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }


        public async Task<StateDTO> AddStateAsync(AddStateDTO addStateDTO)
        {
            try
            {
                var state = mapper.Map<State>(addStateDTO);

                await dbContext.States.AddAsync(state);
                await dbContext.SaveChangesAsync();

                var addedData = await GetStateByIdAsync(state.StateId);

                return mapper.Map<StateDTO>(state);
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

                    var response = mapper.Map<StateDTO>(stateDomain);
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

        public async Task<StateDTO?> UpdateStateByIdAsync(long id, UpdateStateDTO updateStateDTO)
        {
            try
            {
                var stateDomain = await dbContext.States.FirstOrDefaultAsync(x => x.StateId == id);
                if (stateDomain != null)
                {
                    mapper.Map(updateStateDTO, stateDomain);

                    dbContext.Update(stateDomain);
                    await dbContext.SaveChangesAsync();

                    return mapper.Map<StateDTO>(stateDomain);

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
