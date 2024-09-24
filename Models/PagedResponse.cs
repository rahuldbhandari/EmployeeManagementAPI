namespace EmployeeManagementAPI.Models
{
    public class PagedResponse<Entity>
    {
        public PagedResponse(Entity data, int pageNumber, int pageSize, int totalRecords) {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
        public Entity Data { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

    }
}
