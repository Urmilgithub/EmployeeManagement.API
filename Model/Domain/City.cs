using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.Domain
{
    public class City
    {
        [Key]
        public long CityId { get; set; }
        public string? CityName { get; set; }
        public long? StateId { get; set; }
    }
}
