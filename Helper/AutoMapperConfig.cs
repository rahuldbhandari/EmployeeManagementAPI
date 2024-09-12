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


            CreateMap<Employee, EmployeeFetchDTO>()
            /*.ForMember(src => src.ProthomNaam, action => action.MapFrom(dst => dst.FirstName))
            .ForMember(src => src.SeshNaam, action => action.MapFrom(dst => dst.LastName))
            .ForMember(src => src.JonmoTarikh, action => action.MapFrom(dst => dst.BirthDate))
            .ForMember(src => src.Lingo, action => action.MapFrom(dst => dst.Gender))*/
            .ReverseMap();
        }
    }
}
