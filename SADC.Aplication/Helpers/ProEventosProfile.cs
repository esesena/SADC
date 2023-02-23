using AutoMapper;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Domain.Identity;

namespace SADC.Application.Helpers
{
    public class SADCProfile : Profile
    {
        public SADCProfile()
        {
            CreateMap<Farm, FarmDto>().ReverseMap();
            CreateMap<Plot, PlotDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeAddDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}