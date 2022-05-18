using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter student name.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s]*$")]
        [StringLength(64, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
