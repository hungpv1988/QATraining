using StudentAppMvc.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models.ViewModel
{
    public class ClassItemViewModel
    {
        public int? Id { get; set; }
        public string? UpcomingPeriod{ get; set; }

        public List<DepartmentDto>? Departments { get; set; }

        public string DepartmentCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
