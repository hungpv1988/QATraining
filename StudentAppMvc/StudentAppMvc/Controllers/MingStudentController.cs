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

                student.Id = count++;
                _students.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
