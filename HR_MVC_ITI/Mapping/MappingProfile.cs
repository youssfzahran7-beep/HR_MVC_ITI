using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.ViewModels;
using HR_MVC_ITI.ViewModels;
using AutoMapper;

namespace HR_MVC_ITI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Candidate, CandidateViewModel>().ReverseMap();
        CreateMap<Recruitment, RecruitmentViewModel>().ReverseMap();
        CreateMap<ApplicationProcess, ApplicationProcessViewModel>()
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : string.Empty))
            .ForMember(dest => dest.RequirementTitle, opt => opt.MapFrom(src => src.Recruitment != null ? src.Recruitment.Title : string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.Candidate, opt => opt.Ignore())
            .ForMember(dest => dest.Recruitment, opt => opt.Ignore());

        CreateMap<ApplicationInterview, InterviewViewModel>()
            .ForMember(dest => dest.InterviewDate, opt => opt.MapFrom(src => src.ScheduledDate))
            .ForMember(dest => dest.FeedBack, opt => opt.MapFrom(src => src.Feedback))
            .ForMember(dest => dest.ApplicantionName, opt => opt.MapFrom(src =>
                src.ApplicationProcess != null && src.ApplicationProcess.Candidate != null
                    ? src.ApplicationProcess.Candidate.FirstName + " " + src.ApplicationProcess.Candidate.LastName
                    : (src.ApplicationProcess != null ? "Application #" + src.ApplicationProcess.Id : string.Empty)))
            .ReverseMap()
            .ForMember(dest => dest.ScheduledDate, opt => opt.MapFrom(src => src.InterviewDate))
            .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.FeedBack))
            .ForMember(dest => dest.ApplicationProcess, opt => opt.Ignore());

        CreateMap<ApplicationOffer, OfferViewModel>()
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src =>
                src.ApplicationProcess != null && src.ApplicationProcess.Candidate != null
                    ? src.ApplicationProcess.Candidate.FirstName + " " + src.ApplicationProcess.Candidate.LastName
                    : (src.ApplicationProcess != null ? "Application #" + src.ApplicationProcess.Id : string.Empty)))
            .ReverseMap()
            .ForMember(dest => dest.ApplicationProcess, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationInterview, opt => opt.Ignore());

        CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
        CreateMap<Contract, ContractViewModel>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.Employee, opt => opt.Ignore());
        CreateMap<Payroll, PayrollViewModel>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.Employee, opt => opt.Ignore());
        CreateMap<WorkSchedule, WorkScheduleViewModel>().ReverseMap();
    }
}
