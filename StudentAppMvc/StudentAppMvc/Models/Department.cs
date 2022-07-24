using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Department
    {
        public Department()
        {

        }
        public Department(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        [Key]
        [Display(Name = "Code")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Department code's length should be from 1 to 50")]
        [Required(ErrorMessage = "Please input code of department")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please input name of department")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please input description of department")]
        public string Description { get; set; }

        public List<Class>? Classes { get; set; }
    }
}
