// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    [LogFilter]
    public class SubjectController : Controller
    {
        private static List<Subject>? _subjects = new List<Subject>();
        public SubjectController()
        {
            if (_subjects?.Count == 0)
            {
                _subjects.Add(new Subject(_subjects.Count + 1, "Tính toán khoa học", "Tính toán khoa học"));
                _subjects.Add(new Subject(_subjects.Count + 1, "Điện tử số", "Điện tử số"));
                _subjects.Add(new Subject(_subjects.Count + 1, "Vi xử lý", "Vi xử lý"));
                _subjects.Add(new Subject(_subjects.Count + 1, "Trí tuệ nhân tạo", "Trí tuệ nhân tạo"));
                _subjects.Add(new Subject(_subjects.Count + 1, "Xử lý ảnh", "Xử lý ảnh"));
                _subjects.Add(new Subject(_subjects.Count + 1, "Lập trình hướng đối tượng", "Lập trình hướng đối tượng"));
            }
        }

        // Default view for GET list
        public IActionResult Index()
        {
            ViewBag.Subjects = _subjects;
            return View();
        }


        // GET: Create
        public ActionResult Create()
        {
            SubjectViewModel subjectViewModel = new SubjectViewModel(new TeacherController().Teachers);
            return View(subjectViewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {
                if (_subjects == null)
                {
                    _subjects = new List<Subject>();
                }

                // Check if email/ student account is existing, return same view with error message
                if (_subjects.Any(s => s.Name.Equals(subject.Name)))
                    ModelState.AddModelError("Name", "This subject name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(subject);

                subject.Id = _subjects.Count + 1;
                _subjects.Add(subject);

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
            Subject subject = _subjects.FirstOrDefault(s => s.Id == id);
            return View(subject);
        }

        // GET: Create
        public ActionResult Edit(int id)
        {
            Subject subject = _subjects.FirstOrDefault(s => s.Id == id);
            return View(subject);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            try
            {
                Subject editStudent = _subjects.FirstOrDefault(s => s.Id == subject.Id);
                // Check if this student is avalable for update
                if (editStudent == null)
                {
                    ModelState.AddModelError("Name", "This subject is not avaiable for editing. Maybe it was deleted from list.");
                    return View(subject);
                }

                // Check if email/ student account is used by another student
                if (_subjects.Any(s => s.Name.Equals(subject.Name) && s.Id != subject.Id))
                    ModelState.AddModelError("Name", "This subject name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(subject);

                // Update student
                editStudent.Name = subject.Name;
                editStudent.Description = subject.Description;

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
                Subject editSubject = _subjects.FirstOrDefault(s => s.Id == id);
                // Check if this student is avalable for update
                if (editSubject == null)
                {
                    ModelState.AddModelError("Name", "This subject is not avaiable for editing. Maybe it was deleted from list.");
                    return RedirectToAction(nameof(Index));
                }


                // Update student
                int index = _subjects.IndexOf(editSubject);
                _subjects.RemoveAt(index);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
