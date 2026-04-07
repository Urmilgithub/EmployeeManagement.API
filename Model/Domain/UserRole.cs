using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model.Domain
{
    public class UserRole
    {
        [Key]
        public long UseRoleId { get; set; }
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public long RoleId { get; set; }
        
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
