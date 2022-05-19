// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    [LogFilter]
    public class MingStudentController : Controller
    {
        private static List<Student>? _students = new List<Student>();
        public MingStudentController()
        {
            if (_students?.Count == 0)
            {
                _students.Add(new Student(_students.Count + 1, "Minh", "Sample student", DateTime.Now, false, "minh@hut.edu" ));
                _students.Add(new Student(_students.Count + 1, "Bảo", "Sample student", DateTime.Now, false, "bao@hut.edu" ));
                _students.Add(new Student(_students.Count + 1, "Ngọc", "Sample student", DateTime.Now, false, "ngoc@hut.edu" ));
                _students.Add(new Student(_students.Count + 1, "Kang", "Sample student", DateTime.Now, false, "kang@hut.edu" ));
                _students.Add(new Student(_students.Count + 1, "Khanh", "Sample student", DateTime.Now, false, "khanh@hut.edu" ));
                _students.Add(new Student(_students.Count + 1, "Hi", "Sample student", DateTime.Now, false, "hi@hut.edu" ));
            }   
        }

        // Default view for GET list
        public IActionResult Index(int latestCount = 0, string searchName = "")
        {
            latestCount = (latestCount < _students.Count) ? latestCount : _students.Count;
            searchName = searchName.Trim();
            if (latestCount > 0)
                ViewData["Students"] = _students.GetRange(_students.Count - latestCount, latestCount);
            else if (!string.IsNullOrEmpty(searchName))
            {
                // Search from Name, Description and Email
                ViewData["Students"] = _students.Where(s => s.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase) || s.Description.Contains(searchName, StringComparison.OrdinalIgnoreCase) || s.Email.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["searchName"] = searchName;
            }    
            else
                ViewData["Students"] = _students;
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                if (_students == null)
                {
                    _students = new List<Student>();
                }

                // Check if email/ student account is existing, return same view with error message
                if (_students.Any(s => s.Email.Equals(student.Email)))
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(student.StudentAccount) && _students.Any(s => s.StudentAccount.Equals(student.StudentAccount)))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(student);

                student.Id = _students.Count + 1;
                _students.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Detail
        public ActionResult Detail(int id)
        {
            Student student = _students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        // GET: Create
        public ActionResult Edit(int id)
        {
            Student student = _students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                Student editStudent = _students.FirstOrDefault(s => s.Id == student.Id);
                // Check if this student is avalable for update
                if (editStudent == null)
                {
                    ModelState.AddModelError("Name", "This student is not avaiable for editing. Maybe it was deleted from list.");
                    return View(student);
                }    

                // Check if email/ student account is used by another student
                if (_students.Any(s => s.Email.Equals(student.Email) && s.Id != student.Id))
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(student.StudentAccount) && (_students.Any(s => s.StudentAccount.Equals(student.StudentAccount) && s.Id != student.Id)))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(student);

                // Update student
                editStudent.Name = student.Name;
                editStudent.Email = student.Email;
                editStudent.Gender = student.Gender;
                editStudent.Description = student.Description;
                editStudent.DateOfBirth = student.DateOfBirth;
                editStudent.StudentAccount = student.StudentAccount;

                //int index = _students.IndexOf(editStudent);
                //_students[index] = student;

                return RedirectToAction(nameof(Detail), new { id = student.Id } );
            }
            catch
            {
                return View();
            }
        }

        // POST: Delete
        public ActionResult Delete(int id)
        {
            try
            {
                Student editStudent = _students.FirstOrDefault(s => s.Id == id);
                // Check if this student is avalable for update
                if (editStudent == null)
                {
                    ModelState.AddModelError("Name", "This student is not avaiable for editing. Maybe it was deleted from list.");
                    return RedirectToAction(nameof(Index));
                }


                // Update student
                int index = _students.IndexOf(editStudent);
                _students.RemoveAt(index);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
