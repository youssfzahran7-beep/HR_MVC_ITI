using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.ViewModels;
using HR_MVC_ITI.DTOs;
using AutoMapper;

namespace HR_MVC_ITI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Attendance, AttendanceDTO>().ReverseMap();
        CreateMap<Contract, ContractDTO>().ReverseMap();
        CreateMap<Payroll, PayrollDTO>().ReverseMap();
        CreateMap<Candidate, CandidateDTO>().ReverseMap();
        CreateMap<Recruitment, RecruitmentDTO>().ReverseMap();
        CreateMap<ApplicationProcess, ApplicationProcessDTO>().ReverseMap();
        CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
        CreateMap<Contract, ContractViewModel>().ReverseMap();
        CreateMap<Payroll, PayrollViewModel>().ReverseMap();
    }
}
