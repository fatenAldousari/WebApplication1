using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AddEmployeeForm
    {
      
        public string Name { get; set; }
        public string CivilId { get; set; }
        public string Position { get; set; }
     
    }
    public class EditBranchForm
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationURL { get; set; }
        public string BranchManager { get; set; }
        public string EmployeeCount { get; set; }
    }
        public class NewBranchForm
    {
        [Required]
        public int Id { get; set; }
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
