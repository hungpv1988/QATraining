using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Data;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;
using StudentAppMvc.Models.ViewModel;
using StudentAppMvc.Services;

namespace StudentAppMvc.Controllers
{
    [LogFilterAttribute]
    [AuthenticationFilterAttribute]
    public class DepartmentController : Controller
    {
        private ISchoolService _schoolService;

        public DepartmentController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departmentList = _schoolService.ListDepartments();
            var indexViewModel = new DepartmentListViewModel()
            {
                DepartmentList = departmentList
            };

            return View(indexViewModel);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _schoolService.GetDepartment(id);
            return View(viewModel);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Description")] DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            { 
                var createdDepartment = _schoolService.AddDepartment(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var departmentDto = _schoolService.GetDepartment(id);
            if (departmentDto == null)
            {
                return NotFound();
            }

            return View(departmentDto);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string code, [Bind("Code,Name,Description")] DepartmentDto departmentDto)
        {
            if (code != departmentDto.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _schoolService.UpdateDepartment(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var department = _schoolService.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var department = _schoolService.DeleteDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(string code)
        {
          return _schoolService.GetDepartment(code) != null;
        }
    }
}
