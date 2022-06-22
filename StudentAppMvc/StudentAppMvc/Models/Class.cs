using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string DepartmentCode { get; set; }

        public Department? Department { get; set; }
    }
}
