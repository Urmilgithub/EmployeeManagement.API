using EmployeeManagement.Model.Domain;

namespace EmployeeManagement.Model.DTO
{
    public class CityDTO
    {
        public long CityId { get; set; }
        public string? CityName { get; set; }
        public long? StateId { get; set; }
        public string? StateName { get; set;}
    }
}