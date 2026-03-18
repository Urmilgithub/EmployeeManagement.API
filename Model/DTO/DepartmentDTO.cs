using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.Model.DTO
{
    public class DepartmentDTO
    {
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? CityId { get; set; }
        public string CityName {  get; set; } 
    }
}
