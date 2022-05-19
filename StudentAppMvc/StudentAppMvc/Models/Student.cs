// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Student
    {
        public Student(int id, string name, string? description, DateTime? dateOfBirth, bool gender, string email, string studentAccount = null)
        {
            Id = id;
            Name = name;
            Description = description;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            if(string.IsNullOrEmpty(studentAccount))
                StudentAccount = $"HUT{id}";
        }

        public Student()
        {
        }

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage ="Please input Student name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public bool Gender { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@hut.edu$", ErrorMessage = "Email should be @hut.edu")]
        [Required]
        public string Email { get; set; }

        [StringLength(5)]
        public string? StudentAccount { get; set; }
    }
}
