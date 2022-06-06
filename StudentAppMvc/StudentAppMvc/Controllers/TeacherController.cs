// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;
using System.Globalization;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    [LogFilter]
    public class TeacherController : Controller
    {
        private static List<Teacher>? _teachers = new List<Teacher>();

        public List<Teacher> Teachers { get => _teachers; }

        public TeacherController()
        {
            if (_teachers?.Count == 0)
            {
                _teachers.Add(new Teacher(_teachers.Count + 1, "Nguyễn Đức Nghĩa", "KHMT"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Trịnh Văn Loan", "KHMT"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Văn Thế Minh", "KHMT"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Nguyễn Thanh Thủy", "HTTT"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Nguyễn Linh Giang", "MTT"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Huỳnh Quyết Thắng", "CNPM"));
                _teachers.Add(new Teacher(_teachers.Count + 1, "Đỗ Văn Uy", "HTTT"));
            }
        }

        // Default view for GET list
        public IActionResult Index()
        {
            ViewBag.Teachers = _teachers;
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
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                if (_teachers == null)
                {
                    _teachers = new List<Teacher>();
                }

                // Check if email/ student account is existing, return same view with error message
                if (_teachers.Any(s => s.Name.Equals(teacher.Name)))
                    ModelState.AddModelError("Name", "This teacher name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(teacher);

                teacher.Id = _teachers.Count + 1;
                _teachers.Add(teacher);

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
            Teacher teacher = _teachers.FirstOrDefault(s => s.Id == id);
            return View(teacher);
        }

        // GET: Create
        public ActionResult Edit(int id)
        {
            Teacher teacher = _teachers.FirstOrDefault(s => s.Id == id);
            return View(teacher);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            try
            {
                Teacher editTeacher = _teachers.FirstOrDefault(s => s.Id == teacher.Id);
                // Check if this student is avalable for update
                if (editTeacher == null)
                {
                    ModelState.AddModelError("Name", "This teacher is not avaiable for editing. Maybe it was deleted from list.");
                    return View(teacher);
                }

                // Check if email/ student account is used by another student
                if (_teachers.Any(s => s.Name.Equals(teacher.Name) && s.Id != teacher.Id))
                    ModelState.AddModelError("Name", "This teacher name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(teacher);

                // Update student
                editTeacher.Name = teacher.Name;
                editTeacher.Description = teacher.Description;

                return RedirectToAction(nameof(Index));
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
                Teacher editTeacher = _teachers.FirstOrDefault(s => s.Id == id);
                // Check if this student is avalable for update
                if (editTeacher == null)
                {
                    ModelState.AddModelError("Name", "This teacher is not avaiable for editing. Maybe it was deleted from list.");
                    return RedirectToAction(nameof(Index));
                }


                // Update student
                int index = _teachers.IndexOf(editTeacher);
                _teachers.RemoveAt(index);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
