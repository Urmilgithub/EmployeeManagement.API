using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model.Domain
{
    public class Department
    {
        [Key]
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? CityId { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
    }
}
