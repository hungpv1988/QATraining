using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Models;

namespace StudentAppMvc.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> _studentList;
        public StudentController()
        {

        }

        // GET: StudentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateStudentWithoutModel()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                if (_studentList == null) 
                {
                    _studentList = new List<Student>();
                }

                _studentList.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = _studentList.Where(s => s.Id == id);

            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                var oldStudent = _studentList.Where(s => s.Id == student.Id).FirstOrDefault();
                oldStudent.Name = student.Name;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateStudentForQLPham() 
        {
            return View();
        }
    }
}
