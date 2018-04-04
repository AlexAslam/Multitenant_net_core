using AutoMapper;
using MultiTenantCore.DataModels.Entities;
using MultiTenantCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore.DataModels.Mapper
{
    public class MultiTenantCoreMappingProfile : Profile
    {
        public MultiTenantCoreMappingProfile()
        {
            CreateMap<Tenant, TenantViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
