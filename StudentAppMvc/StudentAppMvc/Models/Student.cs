using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {
        [Key]     
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, ErrorMessage = "Name's length should be under 50 ")]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public bool Gender { get; set; }

        [RegularExpression(@"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)", ErrorMessage = "Wrong email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public string? Description { get; set; }

        public int? ClassId { get; set; }
    }
}
