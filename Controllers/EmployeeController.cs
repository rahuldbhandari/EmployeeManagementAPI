using EmployeeManagementAPI.BAL.IServices;
using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.Helper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("v1/Add")]
        public async Task<ResponseModel<int>> Add([FromBody] EmployeeCreateDTO req)
        {
            ResponseModel<int> res = new ResponseModel<int>();
            try
            {
                res.Result = await _employeeService.addService(req);
                res.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                res.Message = Messages.Bad_Request;
                _logger.LogError(ex, "EmployeeController [Add]: " + ex.Message);
            }
            return res;
        }
        //public async Task<PagedResponse<IEnumerable<EmployeeFetchDTO>>> fetchService(PaginationQuery? paginationQuery = null,)
        [HttpPost("v1/GetAll")]
        public async Task<ResponseModel<PagedResponse<IEnumerable<EmployeeFetchDTO>>>> Get([FromQuery]string? deptno, [FromBody]DynamicListQueryModel? dynamicQuery)
        {
            ResponseModel<PagedResponse<IEnumerable<EmployeeFetchDTO>>> res = new ResponseModel<PagedResponse<IEnumerable<EmployeeFetchDTO>>>();
            try
            {
                res.Result = await _employeeService.GetAll(deptno, dynamicQuery);
                res.StatusCode = HttpStatusCode.Found;
            }
            catch (Exception ex)
            {
                res.Message = Messages.Bad_Request;
                _logger.LogError(ex, "EmployeeController [Add]: " + ex.Message);
            }
            return res;
        }
    }
}
