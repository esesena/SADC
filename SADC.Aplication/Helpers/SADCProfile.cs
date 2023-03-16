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
            CreateMap<Field, FieldDto>().ReverseMap();
            CreateMap<Planting, PlantingDto>().ReverseMap();
            CreateMap<Seed, SeedDto>().ReverseMap();
            CreateMap<Notice, NoticeDto>().ReverseMap();
            CreateMap<PlantingField, PlantingFieldDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeesFarms, EmployeesFarmsDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}