using AutoMapper;
using EmployeeManagement.Model.Domain;
using EmployeeManagement.Model.DTO;

namespace EmployeeManagement.Mapper
{
    public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            // Employee Mapping
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<AddEmployeeDTO, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, Employee>().ReverseMap();

        }
    }
}
