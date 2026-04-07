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
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.StateName, 
                    opt => opt.MapFrom(src => src.State != null ? src.State.StateName : null))
                .ForMember(dest => dest.CityName, 
                    opt => opt.MapFrom(src => src.City != null ? src.City.CityName : null))
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null))
                .ForMember(dest => dest.JobTitle,
                    opt => opt.MapFrom(src => src.Job != null ? src.Job.JobTitle : null));

            CreateMap<AddEmployeeDTO, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, Employee>().ReverseMap();


            //State Mapping
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<AddStateDTO,State>().ReverseMap();
            CreateMap<UpdateStateDTO, State>().ReverseMap();


            //City Mapping
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.StateName,
                    opt => opt.MapFrom(src => src.State != null ? src.State.StateName : null));

            CreateMap<AddCityDTO, City>().ReverseMap();
            CreateMap<UpdateCityDTO, City>().ReverseMap();


            // Department Mapping
            CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.CityName,
                    opt => opt.MapFrom(src => src.City != null ? src.City.CityName : null));

            CreateMap<AddDepartmentDTO, Department>().ReverseMap();
            CreateMap<UpdateCityDTO, Department>().ReverseMap();


            // Job Mapping
            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.JobTitle,
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null));

            CreateMap<AddDepartmentDTO, Department>().ReverseMap();
            CreateMap<UpdateCityDTO, Department>().ReverseMap();
        }
    }
}
