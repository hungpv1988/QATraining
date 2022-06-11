// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAppMvc.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel(int id, string name, string? description, DateTime? dateOfBirth, bool gender, string email, string studentAccount = null)
        {
            Id = id;
            Name = name;
            Description = description;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            if (string.IsNullOrEmpty(studentAccount))
                StudentAccount = $"HUT{id}";
        }

        public StudentViewModel()
        {
        }

        public StudentViewModel(int? id = null)
        {
            if (id == null)
                return;

            Student student = MyData.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                throw new Exception("no student found");
            else
            {
                Id = student.Id;
                Name = student.Name;
                Description = student.Description;
                DateOfBirth = student.DateOfBirth;
                Gender = student.Gender;
                Email = student.Email;
                StudentAccount = student.StudentAccount;
                string subjectName = "";
                string teacherName = "";
                foreach(int key in student.AssignedSubjectAndTeachers.Keys)
                {
                    subjectName = MyData.Subjects.Where(s => s.Id == key).FirstOrDefault()?.Name;
                    teacherName = MyData.Teachers.Where(t => t.Id == student.AssignedSubjectAndTeachers[key]).FirstOrDefault()?.Name;
                    _assignedSubjectAndTeacherNames.Add(subjectName, teacherName);

                    SelectedSubjectViewModel subjectViewModel = SelectedSubjects?.FirstOrDefault(s => s.Id == key);
                    if(subjectViewModel != null)
                    {
                        subjectViewModel.Selected = true;
                        subjectViewModel.TeacherId = student.AssignedSubjectAndTeachers[key].ToString();
                    }    
                }    
            }
        }

        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required(ErrorMessage = "Please input Student name")]
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

        private List<SelectedSubjectViewModel> _selectedSubjectViewModels;
        public List<SelectedSubjectViewModel>? SelectedSubjects
        {
            get
            {
                if (_selectedSubjectViewModels == null)
                {
                    _selectedSubjectViewModels = new List<SelectedSubjectViewModel>();
                    foreach (Subject subject in MyData.Subjects)
                        _selectedSubjectViewModels.Add(new SelectedSubjectViewModel(subject.Id));
                }

                return _selectedSubjectViewModels;
            }
        }

        private readonly Dictionary<string, string>? _assignedSubjectAndTeacherNames = new Dictionary<string, string>();
        public Dictionary<string, string> AssignedSubjectAndTeacherNames
        { 
            get 
            {
                return _assignedSubjectAndTeacherNames; 
            } 
        }
    }

    public class SelectedSubjectViewModel
    {
        public SelectedSubjectViewModel() 
        { 
        }

        public SelectedSubjectViewModel(int id, bool selected = false)
        {
            Id = id;
            Selected = selected;

            if (Id > 0 && _assignedTeachers.Count == 0)
            {
                Subject subject = MyData.Subjects.FirstOrDefault(x => x.Id == Id);

                if (subject != null)
                {
                    _assignedTeachers.Add(new SelectListItem("-", "-1"));
                    foreach (int teacherId in subject.AssignedTeachers)
                    {
                        string teacherName = MyData.Teachers.Where(t => t.Id == teacherId).Select(n => n.Name).FirstOrDefault();
                        _assignedTeachers.Add(new SelectListItem(teacherName, teacherId.ToString(), false));
                    }
                }
            }
        }

        public int Id { get; set; }

        public string? Name
        {
            get
            {
                return (Id > 0) ? MyData.Subjects.FirstOrDefault(x => x.Id == this.Id)?.Name : null;
            }
        }

        private List<SelectListItem> _assignedTeachers = new List<SelectListItem>();

        public List<SelectListItem> AssignedTeachers
        {
            get
            {
                if (Id > 0 && _assignedTeachers.Count == 0)
                {
                    Subject subject = MyData.Subjects.FirstOrDefault(x => x.Id == Id);

                    if (subject != null)
                    {
                        _assignedTeachers.Add(new SelectListItem("-", "-1"));
                        foreach (int teacherId in subject.AssignedTeachers)
                        {
                            string teacherName = MyData.Teachers.Where(t => t.Id == teacherId).Select(n => n.Name).FirstOrDefault();
                            _assignedTeachers.Add(new SelectListItem(teacherName, teacherId.ToString(), false));
                        }
                    }
                }

                return _assignedTeachers;
            }
        }

        public string? TeacherId { get; set; } = "-1";

        private SelectList _selectTeacherList;
        public SelectList? SelectTeacherList
        {
            get
            {
                if (_selectTeacherList == null)
                {
                    _selectTeacherList = new SelectList(_assignedTeachers, "Value", "Text", TeacherId);
                }

                return _selectTeacherList;
            }
        }

        public bool Selected { get; set; } = false;
    }
}
