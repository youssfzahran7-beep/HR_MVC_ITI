using HR_MVC_ITI.DTOs;
using HR_MVC_ITI.Models.Enitityes;
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
    }
}
