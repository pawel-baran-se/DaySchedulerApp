using AutoMapper;
using DaySchedulerApp.Application.DTOs.Assignment;
using DaySchedulerApp.Domain;

namespace DaySchedulerApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
            CreateMap<Assignment, AssignmentDetailDto>().ReverseMap();
            CreateMap<Assignment, CreateAssignmentDto>().ReverseMap();
            CreateMap<Assignment, UpdateAssignmentDto>().ReverseMap();
            CreateMap<Assignment, ChangeNotificationSettingsDto>().ReverseMap();
        }
    }
}