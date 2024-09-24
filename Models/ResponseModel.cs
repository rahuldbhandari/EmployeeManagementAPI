using System.Net;

namespace EmployeeManagementAPI.Models
{
    public class ResponseModel<Entity>
    {
        public Entity? Result { get; set; }
        public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string? Message { get; set; }
    }
}
