using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class NewBranchForm
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationURL { get; set; }
        [Required]
        public string BranchManager { get; set; }
        [Required]
        public string EmployeeCount { get; set; }
    }
}
