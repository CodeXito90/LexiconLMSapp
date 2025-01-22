using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>().ReverseMap();
        CreateMap<ApplicationUser, UserDto>().ReverseMap();

        CreateMap<ActivityDto, Activity>().ReverseMap();
        CreateMap<ActivityFileDto, ActivityFile>().ReverseMap();
        CreateMap<ActivityTypeDto, ActivityType>().ReverseMap();

        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<CourseFileDto, CourseFile>().ReverseMap();
        CreateMap<CreateCourseDto, Course>().ReverseMap();

        CreateMap<ModuleFileDto, ModuleFile>().ReverseMap();
        CreateMap<ModuleDto, Module>().ReverseMap();
        CreateMap<CreateModuleDto, Module>();

        CreateMap<CreateActivityDto, Activity>();

        CreateMap<CreateActivityTypeDto, ActivityType>();

        CreateMap<UpdateCourseDto, Course>().ReverseMap();
        CreateMap<UpdateActivityDto, Activity>().ReverseMap();
        CreateMap<UpdateModuleDto, Module>().ReverseMap();


    }
}

