using AutoMapper;
using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.DAL.Entities;

namespace EmployeeManagementAPI.Helper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeFetchDTO>().ReverseMap();
        }
    }
}
