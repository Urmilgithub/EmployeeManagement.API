using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.Model.DTO
{
    public class EmployeeDTO
    {
        public long EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int Contact { get; set; }
        public Decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive {  get; set; }


        public long? StateId { get; set; }
        public string? StateName { get; set; }

        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public long? JobId { get; set; }
        public string? JobTitle { get; set; }

        public long? CityId { get; set; }
        public string? CityName { get; set; }
    }
}