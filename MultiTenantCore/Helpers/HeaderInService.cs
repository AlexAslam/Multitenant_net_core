using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore.Helpers
{
    public class HeaderInService
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        
        public HeaderInService()
        {
        }

        public string GetPropName() {
            return _httpContextAccessor.HttpContext.Request.Headers["HOST"];
        }
        
        
    }
}
