using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.IRepository
{
    public interface ITokenHandlerRepository
    {
       Task<string> CreateTokenAsync(User user);
    }
}
