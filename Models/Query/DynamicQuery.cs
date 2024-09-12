namespace EmployeeManagementAPI.Models.Query
{
    public class DynamicQuery
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;

        public List<FilterQuery>? filterQueries { get; set; }

        public SortParameter? sortParameters { get; set; }
    }

    public class FilterQuery
    {
        public string? Field { get; set; }
        public string? Value { get; set; }
        public string? Operator { get; set; }
    }

    public class SortParameter
    {
        public string? Field { get; set; } = "";
        public string? Order { get; set; } = "ASC";
    }
}
