using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model.Domain
{
    public class Employee
    {
        [Key]
        public long EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int Contact { get; set; }
        public Decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }

        public long? StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        public long? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public long? CityId { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }

        public long? JobId { get; set; }

        [ForeignKey("JobId")]
        public Job? Job { get; set; }

    }
}
