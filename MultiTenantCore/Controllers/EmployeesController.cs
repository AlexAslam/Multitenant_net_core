using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantCore.DataModels.Contexts;
using MultiTenantCore.DataModels.Entities;
using MultiTenantCore.DataModels.Repository;
using MultiTenantCore.ViewModels;

namespace MultiTenantCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            //_employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = _employeeRepository.getAllEmployees();
            //var results = _applicationContext.Employees.ToList();
            return Ok(_mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(results));
        }
    }
}