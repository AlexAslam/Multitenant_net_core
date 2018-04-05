using System.ComponentModel.DataAnnotations;

namespace MultiTenantCore.ViewModels
{
    public class TenantViewModel
    {
        [Required]
        public string SubDomainName { get; set; }
        [Required]
        public string ConnectionStringName { get; set; }
    }
}
