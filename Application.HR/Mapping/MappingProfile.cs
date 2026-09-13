using Application.HR.DTOs;
using AutoMapper;
using Domain.HR.Enitityes;
using HRSystem.Domain.Entities;

namespace Application.HR.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Department, DepartmentDTO>().ReverseMap();
        CreateMap<Attendance, AttendanceDTO>().ReverseMap();
        CreateMap<Contract, ContractDTO>().ReverseMap();
        CreateMap<Payroll, PayrollDTO>().ReverseMap();
    }
}
