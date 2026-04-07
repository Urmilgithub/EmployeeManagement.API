using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model.Domain
{
    public class City
    {
        [Key]
        public long CityId { get; set; }
        public string? CityName { get; set; }
        public long? StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }
    }
}
