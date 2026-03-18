using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model.Domain
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email{get; set;}
        public string Password{get; set;}
        public string FirstName{get; set;}
        public string LastName{get; set;}

        [NotMapped]
        public List<string> Roles { get; set;}

        public List<UserRole> UserRoles { get; set; }

    }
}
