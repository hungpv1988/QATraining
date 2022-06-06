// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models
{
    public class Teacher
    {
        public Teacher(int id, string name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Teacher()
        {
        }

        public int Id { get; set; }

        [Display(Name = "Teacher Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage ="Please input Teacher name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool Gender { get; set; }

        public List<string> Subjects { get; set; } = new List<string>();
    }
}
