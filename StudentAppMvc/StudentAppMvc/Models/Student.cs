using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {
     
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public int DateOfBirth { get; set; }

        public bool Gender { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@hut.edu$")]
        [Required]
        public string Email { get; set; }

        [StringLength(5)]
        public string StudentAccount { get; set; }
    }
}
