
using EmployeeManagement.Data;
using EmployeeManagement.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.IRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if(user != null)
            {
                var userRoles = await dbContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
                if(userRoles.Any())
                {
                    user.Roles = new List<string>();
                    foreach(var userRole in userRoles)
                    {
                        var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == userRole.RoleId);
                        if(role != null)
                        {
                            user.Roles.Add(role.RoleType);
                        }

                    }
                }
                
                user.Password = null;
                return user;
            }
            else
            {
                return null;
            }
        }
    } 
}
