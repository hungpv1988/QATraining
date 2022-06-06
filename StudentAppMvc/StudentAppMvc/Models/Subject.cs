// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Subject
    {
        public Subject(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
            AssignedTeachers.Add(1, "Nguyễn Đức Nghĩa");
        }

        public Subject()
        {
            AssignedTeachers.Add(1, "Nguyễn Đức Nghĩa");
        }

        public int Id { get; set; }

        [Display(Name = "Subject Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage ="Please input Subject name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public Dictionary<int, string> AssignedTeachers { get; set; } = new Dictionary<int, string>();
    }
}
