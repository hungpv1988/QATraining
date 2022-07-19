using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Subject Name")]
        [Required(ErrorMessage = "Please input subject name")]

        public string Name { get; set; }
    }
}
