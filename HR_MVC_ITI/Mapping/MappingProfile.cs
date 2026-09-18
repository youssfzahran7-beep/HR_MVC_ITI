using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.ViewModels;
using AutoMapper;

namespace HR_MVC_ITI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
      
        CreateMap<Candidate, CandidateViewModel>().ReverseMap();
        CreateMap<Recruitment, RecruitmentViewModel>().ReverseMap();
        CreateMap<ApplicationProcess, ApplicationProcessViewModel>().ReverseMap();
        CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
        CreateMap<Contract, ContractViewModel>().ReverseMap();
        CreateMap<Payroll, PayrollViewModel>().ReverseMap();
        CreateMap<WorkSchedule, WorkScheduleViewModel>().ReverseMap();
    }
}
