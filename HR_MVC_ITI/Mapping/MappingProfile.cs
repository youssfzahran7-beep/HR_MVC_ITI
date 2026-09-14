using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.ViewModels;
using AutoMapper;

namespace HR_MVC_ITI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
        CreateMap<Contract, ContractViewModel>().ReverseMap();
        CreateMap<Payroll, PayrollViewModel>().ReverseMap();
    }
}
