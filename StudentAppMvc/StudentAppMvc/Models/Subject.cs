using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Subject Name")]
        [Required]
        public string Name { get; set; }
    }
}
