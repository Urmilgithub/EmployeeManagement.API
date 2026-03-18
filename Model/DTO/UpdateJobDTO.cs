namespace EmployeeManagement.Model.DTO
{
    public class UpdateJobDTO
    {
        public string? JobTitle { get; set; }
        public Decimal MinSalary { get; set; }
        public Decimal MaxSalary { get; set; }

        public long? DepartmentId { get; set; }
    }
}
