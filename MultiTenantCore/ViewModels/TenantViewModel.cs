using System.ComponentModel.DataAnnotations;

namespace MultiTenantCore.ViewModels
{
    public class TenantViewModel
    {
        [Required]
        public string SubDomainName { get; set; }
        public string ConnectionStringName { get; set; }
    }
}
