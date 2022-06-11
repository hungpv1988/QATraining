// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Data;
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
            
        }

        // Default view for GET list
        public IActionResult Index()
        {
            ViewBag.Subjects = MyData.Subjects;
            return View();
        }


        // GET: Create
        public ActionResult Create()
        {
            SubjectViewModel subjectViewModel = new SubjectViewModel(null);
            return View(subjectViewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectViewModel subjectViewModel)
        {
            var _subjects = MyData.Subjects;
            try
            {
                // Check if email/ student account is existing, return same view with error message
                if (_subjects.Any(s => s.Name.Equals(subjectViewModel.Name)))
                    ModelState.AddModelError("Name", "This subject name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(subjectViewModel);

                Subject subject = new Subject();
                subject.Id = _subjects.Count + 1;
                subject.Name = subjectViewModel.Name;
                subject.Description = subjectViewModel.Description;
                foreach(SelectedTeacherViewModel t in subjectViewModel.SelectedTeachers)
                {
                    if (t.Selected)
                        subject.AssignedTeachers.Add(t.Id);
                }    
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
            SubjectViewModel subjectViewModel = new SubjectViewModel(id);
            return View(subjectViewModel);
        }

        // GET: Create
        public ActionResult Edit(int id)
        {
            SubjectViewModel subjectViewModel = new SubjectViewModel(id);
            return View(subjectViewModel);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectViewModel subjectViewModel)
        {
            var _subjects = MyData.Subjects;
            try
            {
                Subject editSubject = _subjects.FirstOrDefault(s => s.Id == subjectViewModel.Id);
                // Check if this student is avalable for update
                if (editSubject == null)
                {
                    ModelState.AddModelError("Name", "This subject is not avaiable for editing. Maybe it was deleted from list.");
                    return View(subjectViewModel);
                }

                if (ModelState.ErrorCount > 0)
                    return View(subjectViewModel);

                // Update student
                editSubject.Name = subjectViewModel.Name;
                editSubject.Description = subjectViewModel.Description;
                editSubject.AssignedTeachers = new List<int>();
                foreach (SelectedTeacherViewModel t in subjectViewModel.SelectedTeachers)
                {
                    if (t.Selected)
                        editSubject.AssignedTeachers.Add(t.Id);
                }

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
            var _subjects = MyData.Subjects;
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
