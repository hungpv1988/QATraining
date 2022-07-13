﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Data;
using StudentAppMvc.Models;
using PagedList;
using StudentAppMvc.Models.ViewModel;
using StudentAppMvc.Filter;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilterAttribute]
    public class TahhStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PAGESIZE = 4;

        public TahhStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TahhStudents
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, int? classId)
        {

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "name_asc" : sortOrder;

            //if (searchString != null)
            //{
            //    page = 1;
            //    searchString = convertToUnSign3(searchString);
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            var students = _context.Student != null ? new List<Student>(await _context.Student.ToListAsync()) : null;
            var classes = toDictionary(_context.Class != null ? new List<Class>(await _context.Class.ToListAsync()) : new List<Class>());
            if (students != null)
            {
                students.ForEach(s => s.ClassName = s.ClassId != null ? classes[s.ClassId.ToString()] : "");

                if (!String.IsNullOrEmpty(searchString))
                {
                    students = students.Where(s => convertToUnSign3(s.Name).Contains(convertToUnSign3(searchString))).ToList();
                }

                if (classId >= 0)
                {
                    students = students.Where(s => s.ClassId == classId).ToList();
                }

                if (classId?.ToString() == StudentListViewModal.NO_CLASS_ID)
                {
                    students = students.Where(s => s.ClassId == null).ToList();
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.Name).ToList();
                        break;
                    default:  // Name ascending 
                        students = students.OrderBy(s => s.Name).ToList();
                        break;
                }

                int totalPage = (int)Math.Ceiling((double)students.Count / (double)PAGESIZE);
                int totalStudents = students.Count;
                int pageNumber = (page ?? 1);
                pageNumber = totalStudents <= (pageNumber - 1) * PAGESIZE ? 1 : pageNumber;
                var pagedStudents = students.Take(new Range((pageNumber - 1) * PAGESIZE, Math.Min(totalStudents, pageNumber * PAGESIZE))).ToList();

                return View(new StudentListViewModal(pagedStudents, totalPage, pageNumber, searchString, sortOrder, classId, _context));
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Student'  is null.");
            }
        }

        // GET: TahhStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: TahhStudents/Create
        public IActionResult Create()
        { 
            StudentViewModal studentViewModal = new StudentViewModal(_context);

            return View(studentViewModal);
        }

        // POST: TahhStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfBirth,Gender,Email,Description,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: TahhStudents/Edit/5
        public async Task<IActionResult> Edit(int? id, string? sortOrder, string? searchString, int? page)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentViewModal = new StudentViewModal(_context, student);
            studentViewModal.NameSortOrder = sortOrder;
            studentViewModal.SearchString = searchString;
            studentViewModal.CurrentPage = page;

            return View(studentViewModal);
        }

        // POST: TahhStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfBirth,Gender,Email,Description,ClassId")] Student student, String? CurrentPage, String? NameSortOrder, String? SearchString)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new
                {
                    sortOrder = NameSortOrder,
                    searchString = SearchString,
                    page = CurrentPage
                });
            }

            return View(new StudentViewModal(_context, student));
        }

        // GET: TahhStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: TahhStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }

        public Dictionary<string, string> toDictionary(List<Class> classes)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            classes.ForEach(c =>
            {
                dictionary[c.Id.ToString()] = c.Name;
            });
            return dictionary;
        }
    }
}
