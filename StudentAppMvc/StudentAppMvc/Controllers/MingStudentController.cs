using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Models;

namespace StudentAppMvc.Controllers
{
    public class MingStudentController : Controller
    {
        private static List<Student>? _students = new List<Student>();
        private static int count = 1;
        public MingStudentController()
        {
            if (_students?.Count == 0)
            {
                _students.Add(new Student()
                {
                    Id = count++,
                    Name = "Minh",
                    Description = "Sample student",
                    DateOfBirth = DateTime.Now,
                    Gender = false,
                    Email = "minh@hut.edu",
                    StudentAccount = "STDx0"
                });
                _students.Add(new Student()
                {
                    Id = count++,
                    Name = "Bảo",
                    Description = "Sample student",
                    DateOfBirth = DateTime.Now,
                    Gender = true,
                    Email = "bao@hut.edu",
                    StudentAccount = "STDx1"
                });
            }    
        }

        // Default view for GET list
        public IActionResult Index()
        {
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
                if (_students.Where(s => s.Email == student.Email).FirstOrDefault() != null)
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(student.StudentAccount) && (_students.Where(s => s.StudentAccount == student.StudentAccount).FirstOrDefault() != null))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(student);

                student.Id = count++;
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

        // POST: Create
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
                if (_students.Where(s => s.Email == student.Email && s.Id != student.Id).FirstOrDefault() != null)
                    ModelState.AddModelError("Email", "This email address is existing.");
                if (!string.IsNullOrEmpty(student.StudentAccount) && (_students.Where(s => s.StudentAccount == student.StudentAccount && s.Id != student.Id).FirstOrDefault() != null))
                    ModelState.AddModelError("StudentAccount", "This student account is existing.");

                if (ModelState.ErrorCount > 0)
                    return View(student);

                // Update student
                int index = _students.IndexOf(editStudent);
                _students[index] = student;

                return RedirectToAction(nameof(Detail), new { id = student.Id } );
                //return RedirectToAction(nameof(Index));
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
