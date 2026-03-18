using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.IRepository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
