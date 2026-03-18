using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.Domain
{
    public class Role
    {
        [Key]
        public long RoleId { get; set; }
        public string? RoleType { get; set; }
    }
}
