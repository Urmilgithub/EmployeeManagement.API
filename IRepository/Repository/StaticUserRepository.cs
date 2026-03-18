using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.IRepository.Repository
{
    public class StaticUserRepository: IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Read Only",
            //    LastName = "User",
            //    Email = "readonly@user.com",
            //    Id = 1,
            //    Username = "readonly@user.com",
            //    Password = "Readonly@user",
            //    Roles = new List<string> { "reader" }
            //},
            //new User()
            //{
            //    FirstName = "Read Write",
            //    LastName = "User",
            //    Email = "readwrite@user.com",
            //    Id = 2,
            //    Username = "readwrite@user.com",
            //    Password = "Readwrite@user",
            //    Roles = new List<string> { "reader", "writer" }
            //}
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
