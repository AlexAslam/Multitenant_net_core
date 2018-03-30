using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantCore.DataModels.Entities;
using MultiTenantCore.DataModels.Repository;
using MultiTenantCore.Helpers;
using MultiTenantCore.ViewModels;

namespace MultiTenantCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TenantController : Controller
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        public TenantController(ITenantRepository tenantRepository,IMapper mapper)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = _tenantRepository.getAllTenants();
            var getHeaders = new HeaderInService();
            //var proname = getHeaders.GetPropName();

            return Ok(_mapper.Map<IEnumerable<Tenant>, IEnumerable<TenantViewModel>>(results));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var get_tenant = _tenantRepository.getTenantById(id);
                if (get_tenant != null)
                {
                    return Ok(_mapper.Map<Tenant, TenantViewModel>(get_tenant));
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]TenantViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTenant = _mapper.Map<TenantViewModel, Tenant>(model);
                    _tenantRepository.addEntity(newTenant);
                    return Created($"api/Tenant/{newTenant.Id}", _mapper.Map<Tenant, TenantViewModel>(newTenant));
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest($"There is a issue : {ex}");
            }
        }
    }
}