// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models.ViewModels
{
    public class SubjectViewModel
    {
        public SubjectViewModel(int id, string name, string? description, List<Teacher> availableTeachers)
        {
            Id = id;
            Name = name;
            Description = description;
            AvailableTeachers = availableTeachers;
            Teachers.Add(1, "Nguyễn Đức Nghĩa");
        }

        public SubjectViewModel(List<Teacher>? availableTeachers = null)
        {
            AvailableTeachers = availableTeachers;
            Teachers.Add(1, "Nguyễn Đức Nghĩa");
        }

        public int Id { get; set; }

        [Display(Name = "Subject Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage = "Please input Subject name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public Dictionary<int, string> Teachers { get; set; } = new Dictionary<int, string>();

        public List<Teacher> AvailableTeachers { get; set; } = new List<Teacher>();
    }
}
