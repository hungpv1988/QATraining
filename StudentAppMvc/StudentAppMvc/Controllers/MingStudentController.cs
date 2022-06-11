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
    public class MingStudentController : Controller
    {
        public MingStudentController()
        {
        }

        // Default view for GET list
        public IActionResult Index(int latestCount = 0)
        {
            StudentListViewModel studentsViewModel = new StudentListViewModel(MyData.Students);
            return View(studentsViewModel);
        }
        
        public IActionResult Search(string searchName = "", string searchGender = "", string searchFromDate = "")
        {
            List<Student> students = MyData.Students;
            
            if (!string.IsNullOrEmpty(searchName))
            {
                searchName = searchName.Trim();
                students = MyData.Students.Where(s => s.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchGender))
            {
                bool gender = bool.Parse(searchGender);
                students = students.Where(s => s.Gender == gender).ToList();
            }    
            if(!string.IsNullOrEmpty(searchFromDate))
            {
                DateTime fromDate = DateTime.ParseExact(searchFromDate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                students = students.Where(s => s.DateOfBirth > fromDate).ToList();
            }

            StudentListViewModel studentsViewModel = new StudentListViewModel(students, searchName: searchName, searchGender: searchGender, searchDOB: searchFromDate, isSearchView: true);
            return View(studentsViewModel);
        }

        #region Create Student
        // GET: Create
        public ActionResult Create()
        {
            StudentViewModel studentViewModel = new StudentViewModel(null);
            return View(studentViewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            var _students = MyData.Students;
            try
            {
                // Check if email/ student account is existing, return same view with error message
                if (_students.Any(s => s.Email.Equals(studentViewModel.Email)))
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(studentViewModel.StudentAccount) && _students.Any(s => s.StudentAccount.Equals(studentViewModel.StudentAccount)))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(studentViewModel);

                Student newStudent = new Student(_students.Count+1, studentViewModel.Name, studentViewModel.Description, studentViewModel.DateOfBirth, studentViewModel.Gender, studentViewModel.Email, studentViewModel.StudentAccount);
                newStudent.AssignedSubjectAndTeachers = new Dictionary<int, int>();
                foreach(SelectedSubjectViewModel subject in studentViewModel.SelectedSubjects)
                {
                    if(subject.Selected)
                    {
                        if (subject.TeacherId == null)
                            newStudent.AssignedSubjectAndTeachers.Add(subject.Id, -1);
                        else
                            newStudent.AssignedSubjectAndTeachers.Add(subject.Id, int.Parse(subject.TeacherId));
                    }    
                }    

                _students.Add(newStudent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        // GET: Detail
        public ActionResult Detail(int id)
        {
            StudentViewModel studentViewModel = new StudentViewModel(id);
            return View(studentViewModel);
        }

        #region Edit Student
        // GET: Create
        public ActionResult Edit(int id)
        {
            StudentViewModel studentViewModel = new StudentViewModel(id);
            return View(studentViewModel);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel studentViewModel)
        {
            var _students = MyData.Students;
            try
            {
                Student editStudent = _students.FirstOrDefault(s => s.Id == studentViewModel.Id);
                // Check if this student is avalable for update
                if (editStudent == null)
                {
                    ModelState.AddModelError("Name", "This student is not avaiable for editing. Maybe it was deleted from list.");
                    return View(studentViewModel);
                }    

                // Check if email/ student account is used by another student
                if (_students.Any(s => s.Email.Equals(studentViewModel.Email) && s.Id != studentViewModel.Id))
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(studentViewModel.StudentAccount) && (_students.Any(s => s.StudentAccount.Equals(studentViewModel.StudentAccount) && s.Id != studentViewModel.Id)))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(studentViewModel);

                // Update student
                editStudent.Name = studentViewModel.Name;
                editStudent.Email = studentViewModel.Email;
                editStudent.Gender = studentViewModel.Gender;
                editStudent.Description = studentViewModel.Description;
                editStudent.DateOfBirth = studentViewModel.DateOfBirth;
                editStudent.StudentAccount = studentViewModel.StudentAccount;

                editStudent.AssignedSubjectAndTeachers = new Dictionary<int, int>();
                foreach (SelectedSubjectViewModel subject in studentViewModel.SelectedSubjects)
                {
                    if (subject.Selected)
                    {
                        if (subject.TeacherId == null)
                            editStudent.AssignedSubjectAndTeachers.Add(subject.Id, -1);
                        else
                            editStudent.AssignedSubjectAndTeachers.Add(subject.Id, int.Parse(subject.TeacherId));
                    }
                }

                return RedirectToAction(nameof(Detail), new { id = editStudent.Id } );
            }
            catch
            {
                return View();
            }
        }
        #endregion

        // POST: Delete
        public ActionResult Delete(int id)
        {
            var _students = MyData.Students;
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
