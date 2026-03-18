using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.Model.DTO
{
    public class UpdateEmployeeDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int Contact { get; set; }
        public Decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }


        public long? StateId { get; set; }
        public long? DepartmentId { get; set; }
        public long? JobId { get; set; }
        public long? CityId { get; set; }
    }
}
