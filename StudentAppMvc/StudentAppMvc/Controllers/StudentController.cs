using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    public class StudentController : Controller
    {
        private static List<Student> _studentList;
        public StudentController()
        {

        }

        // GET: StudentController
        public ActionResult Index()
        {
            var studentList = new List<Student>()
            {
                new Student(){ Name = "student 1", Description = "I am an optimistic, candid, responsible and social person. I am confident with my thinking analysis that I can convince people with my points. I am self-reliant, well behaved and above all, a person of strong character. I take initiative whenever the situation arises and come off with flying colours", Id = 1},
                new Student(){ Name = "student 2", Description = "I think that I am a responsible and honest boy/girl who wants to do things successfully. I am punctual towards my work and do it before time. I believe that mutual cooperation is a way to success and like to help people whenever they seek my help. I am an average student and like to read books and play chess.", Id = 2}
            };
            return View(studentList);
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
