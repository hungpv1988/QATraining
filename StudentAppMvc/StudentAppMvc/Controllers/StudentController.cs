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
            if (_studentList == null)
            {
                _studentList = new List<Student>();
                _studentList.Add(new Student() { Name = "Student 1", Description = "I am an optimistic, candid, responsible and social person. I am confident with my thinking analysis that I can convince people with my points. I am self-reliant, well behaved and above all, a person of strong character. I take initiative whenever the situation arises and come off with flying colours",Email="student1@test.com",Id = 1 });
            }
            
            return View(_studentList);

        }

        
        // GET: StudentController/Create
        public ActionResult Create()
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


                if (_studentList.Where(s => s.Email == student.Email).FirstOrDefault() != null)
                {
                    ModelState.AddModelError("Email", "This email address is existing.");
                }

                if (ModelState.ErrorCount > 0)
                    return View(student);

                student.Id = _studentList.Count() + 3;
                _studentList.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Detail
        public ActionResult Details(int id)
        {
            Student student = _studentList.FirstOrDefault(s => s.Id == id);
            return View(student);
        }


        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            Student student = _studentList.FirstOrDefault(s => s.Id == id);

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

                if (_studentList.FirstOrDefault(s => s.Email == student.Email) != null && student.Email != oldStudent.Email)
                {
                    ModelState.AddModelError("Email", "This email address is existing.");
                }

                if (ModelState.ErrorCount > 0)
                    return View(student);

                oldStudent.Name = student.Name;
                oldStudent.Description = student.Description;
                oldStudent.Email = student.Email;

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
            Student student = _studentList.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Student student = _studentList.FirstOrDefault(s => s.Id == id);
                _studentList.Remove(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
