namespace EmployeeManagement.Model.DTO
{
    public class JobDTO
    {
        public long JobId { get; set; }
        public string? JobTitle { get; set; }
        public Decimal MinSalary { get; set; }
        public Decimal MaxSalary { get; set; }

        public long? DepartmentId { get; set; }
        public string? DepartmentName {  get; set; }
    }
}
