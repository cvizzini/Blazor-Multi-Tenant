using System.ComponentModel.DataAnnotations;

namespace ExampleApp.Shared.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Host { get; set; }      
    }
}
