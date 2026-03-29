namespace EmployeeManagement.Model.DTO
{
    public class PaginatedResultDTO<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextpage => PageNumber < TotalPages;
    }
}
