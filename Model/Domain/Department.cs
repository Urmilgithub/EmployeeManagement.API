using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.Domain
{
    public class Department
    {
        [Key]
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? CityId { get; set; }
    }
}
