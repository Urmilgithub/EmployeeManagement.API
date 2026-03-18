using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.Domain
{
    public class UserRole
    {
        [Key]
        public long UseRoleId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
