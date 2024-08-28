namespace EmployeeManagementAPI.Models.Query
{
    public class DynamicQuery
    {
        public PaginationQuery? paginationQueries { get; set; }
        public List<FilterQuery>? filterQueries { get; set; }
    }
}
