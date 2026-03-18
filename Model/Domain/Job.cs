using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.Domain
{
    public class Job
    {
        [Key]
        public long JobId { get; set; }
        public string? JobTitle { get; set; }
        public Decimal MinSalary { get; set; }
        public Decimal MaxSalary { get; set;}

        public long? DepartmentId { get; set; }
    }
}
