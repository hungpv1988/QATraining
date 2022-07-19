using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage = "Please input Student name")]
        public string Name { get; set; }

        [Display(Name = "Description for Student")]
        public string Description { get; set; }

        public string Department { get; set; }

        public bool Gender { get; set; }

        [DisplayFormat(DataFormatString = "MM/dd/yyyy hh:mm tt")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@ptit.edu$", ErrorMessage = "Email should be @ptit.edu")]
        [Required]
        public string Email { get; set; }

        [StringLength(5)]
        public string? StudentAccount { get; set; }
    }
}
