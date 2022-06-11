// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAppMvc.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models.ViewModels
{
    public class SubjectViewModel
    {
        public SubjectViewModel()
        {
        }

        public SubjectViewModel(int? id = null)
        {
            if(id == null) // Create
            {
                foreach (Teacher teacher in MyData.Teachers)
                {
                    SelectedTeachers.Add(new SelectedTeacherViewModel(teacher.Id, teacher.Name, false));
                }
            }
            else
            {
                Subject subject = MyData.Subjects.FirstOrDefault(s => s.Id == id);
                if (subject == null)
                    throw new Exception("No subject found");
                else
                {
                    Id = (int)id;
                    Name = subject.Name;
                    Description = subject.Description;
                    foreach (Teacher teacher in MyData.Teachers)
                    {
                        bool selected = subject.AssignedTeachers.Contains(teacher.Id);
                        SelectedTeachers.Add(new SelectedTeacherViewModel(teacher.Id, teacher.Name, selected));
                    }
                }    
            }    
        }

        public int Id { get; set; }

        [Display(Name = "Subject Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage = "Please input Subject name")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<SelectedTeacherViewModel> SelectedTeachers { get; set; } = new List<SelectedTeacherViewModel>();
    }

    public class SelectedTeacherViewModel
    {
        public SelectedTeacherViewModel(int id, string name, bool selected)
        {
            Id = id;
            Name = name;
            Selected = selected;
        }

        public SelectedTeacherViewModel()
        { }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; } = false;
    }
}
