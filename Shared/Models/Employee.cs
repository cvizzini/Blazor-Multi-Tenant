using System.ComponentModel.DataAnnotations;

namespace ExampleApp.Shared.Models
{
    public class Employee : BaseModel
    {      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string City { get; set; }
    }
}
