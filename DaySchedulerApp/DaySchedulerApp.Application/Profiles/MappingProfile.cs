using AutoMapper;
using DaySchedulerApp.Application.DTOs;
using DaySchedulerApp.Domain;

namespace DaySchedulerApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
            CreateMap<Assignment, AssignmentDetailDto>().ReverseMap();
        }
    }
}