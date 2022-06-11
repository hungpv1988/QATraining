// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Data;
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
        public List<Teacher> Teachers { get => MyData.Teachers; }

        public TeacherController()
        {
            
        }

        // Default view for GET list
        public IActionResult Index()
        {
            ViewBag.Teachers = Teachers;
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
                // Check if email/ student account is existing, return same view with error message
                if (Teachers.Any(s => s.Name.Equals(teacher.Name)))
                    ModelState.AddModelError("Name", "This teacher name is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(teacher);

                teacher.Id = Teachers.Count + 1;
                Teachers.Add(teacher);

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
            Teacher teacher = Teachers.FirstOrDefault(s => s.Id == id);
            return View(teacher);
        }

        // GET: Create
        public ActionResult Edit(int id)
        {
            Teacher teacher = Teachers.FirstOrDefault(s => s.Id == id);
            return View(teacher);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            try
            {
                Teacher editTeacher = Teachers.FirstOrDefault(s => s.Id == teacher.Id);
                // Check if this student is avalable for update
                if (editTeacher == null)
                {
                    ModelState.AddModelError("Name", "This teacher is not avaiable for editing. Maybe it was deleted from list.");
                    return View(teacher);
                }

                // Check if email/ student account is used by another student
                if (Teachers.Any(s => s.Name.Equals(teacher.Name) && s.Id != teacher.Id))
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
                Teacher editTeacher = Teachers.FirstOrDefault(s => s.Id == id);
                // Check if this student is avalable for update
                if (editTeacher == null)
                {
                    ModelState.AddModelError("Name", "This teacher is not avaiable for editing. Maybe it was deleted from list.");
                    return RedirectToAction(nameof(Index));
                }


                // Update student
                int index = Teachers.IndexOf(editTeacher);
                Teachers.RemoveAt(index);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
