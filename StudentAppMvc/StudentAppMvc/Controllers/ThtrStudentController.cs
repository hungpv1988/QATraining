using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Utility;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    public class ThtrStudentController : Controller
    {
        private static List<Student> _studentList;
        private static int count = 1;
        public ThtrStudentController()
        {
            if (_studentList == null)
            {
                _studentList = new List<Student>();
                Student st1 = new Student()
                {
                    Id = count++,
                    Name = "Thu",
                    Description = "desc",
                    Department = "dep1",
                    Email = "thu@ptit.edu"
                };
                Student st2 = new Student()
                {
                    Id = count++,
                    Name = "Trang",
                    Description = "Trang desc",
                    Department = "dep2",
                    Email = "trang@ptit.edu"
                };
                Student st3 = new Student()
                {
                    Id = count++,
                    Name = "Duong",
                    Description = "Duong desc",
                    Department = "dep2",
                    Email = "duong@ptit.edu"
                };

                Student st4 = new Student()
                {
                    Id = count++,
                    Name = "Nga",
                    Description = "Nga desc",
                    Department = "dep3",
                    Email = "Nga@ptit.edu"
                };

                Student st5 = new Student()
                {
                    Id = count++,
                    Name = "Mai",
                    Description = "Mai desc",
                    Department = "dep3",
                    Email = "Mai@ptit.edu"
                };

                Student st6 = new Student()
                {
                    Id = count++,
                    Name = "Hoa",
                    Description = "Hoa desc",
                    Department = "dep3",
                    Email = "Hoa@ptit.edu",
                    StudentAccount = "9092213123"

                };

                _studentList.Add(st1);
                _studentList.Add(st2);
                _studentList.Add(st3);
                _studentList.Add(st4);
                _studentList.Add(st5);
                _studentList.Add(st6);

            }
        }
        // GET: StudentController
        public ActionResult Index(int lastestCount = 0)
        {
            if (lastestCount > 0 && _studentList.Count >= 5)
            {
                ViewBag.StudentList = _studentList.OrderByDescending(st => st.Id).ToList().GetRange(0, 5);
            }
            else
                ViewBag.StudentList = _studentList;

            return View();
        }

        public ActionResult Search(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                ViewBag.StudentList = _studentList.Where(st => st.Name.ContainsCaseInsensitive(keyword) || st.Description.ContainsCaseInsensitive(keyword) ||
               st.Department.ContainsCaseInsensitive(keyword) || st.Email.ContainsCaseInsensitive(keyword)).ToList();
                // how to check StudentAccount 
            }
            else
                ViewBag.StudentList = _studentList;

            ViewBag.Keyword = keyword;
            return View();
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            Student student = GetStudentById(id);
            ViewBag.Student = student;
            return View();
            //return View(student);
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
                student.Id = count++;
                if (checkDuplicateEmail(student.Email))
                {
                    ModelState.AddModelError("Email", "Email is existent!");
                    return View(student);
                }

                _studentList.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Student student = GetStudentById(id);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Student student)
        public ActionResult Edit(Student student)
        {

            Student oldStudent = GetStudentById(student.Id);

            if (checkDuplicateEmail(student.Email, oldStudent))
            {
                ModelState.AddModelError("Email", "Email is existent!");
                return View(student);
            }

            oldStudent.Name = student.Name;
            oldStudent.Email = student.Email;
            oldStudent.Department = student.Department;
            oldStudent.Description = student.Description;
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            Student student = GetStudentById(id);
            _studentList.Remove(student);
            //_studentList.RemoveAll(st => st.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private Student GetStudentById(int id)
        {
            return _studentList.FirstOrDefault(st => st.Id == id);
        }

        private bool checkDuplicateEmail(string email, Student? student = null)
        {
            Student getStudent = _studentList.FirstOrDefault(st => st.Email.Equals(email));

            if (getStudent == null || (student != null && getStudent.Id.Equals(student.Id)))
            {
                return false;
            }

            return true;

        }
    }
}
