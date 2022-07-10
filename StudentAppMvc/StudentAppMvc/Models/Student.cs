using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {
        public Student()
        {
        }

        public Student(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            DateOfBirth = student.DateOfBirth;
            Gender = student.Gender;
            Email = student.Email;
            Description = student.Description;
            ClassId = student.ClassId;
            ClassName = student.ClassName;
        }

        public Student(int id, string name, DateTime? dateOfBirth, bool gender, string email, string? description, int? classId, string? className)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            Description = description;
            ClassId = classId;
            ClassName = className;
        }

        [Key]     
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, ErrorMessage = "Name's length should be under 50 ")]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Male")]
        public bool Gender { get; set; }

        [RegularExpression(@"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)", ErrorMessage = "Wrong email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public string? Description { get; set; }

        public int? ClassId { get; set; }

        [Display(Name = "Class")]
        public string? ClassName { get; set; }



        
    }
}
