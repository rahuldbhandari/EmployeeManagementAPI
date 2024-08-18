using System.Net;

namespace EmployeeManagementAPI.Models
{
    public class ResponseModel<T>
    {
        public T? Result { get; set; }
        public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string? Message { get; set; }
    }
}
