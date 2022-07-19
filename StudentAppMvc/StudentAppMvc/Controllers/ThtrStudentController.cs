using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;

namespace StudentAppMvc.Controllers
{
    //[AuthenticationFilter]
    [LogginFilter]
    public class ThtrStudentController : Controller
    {
        private static List<Student> _studentLst;
        private static List<TotalMark> _totakMarkList;
        public static List<MarkForStudent> _markForStudentList;
        public static List<StudentMarkViewModel> _studentMarksList;
        private static StudentListViewModel _studentListViewModel;
        private static int count = 1;
        private static int markForStudentCount = 1;
        public ThtrStudentController()
        {
            if (_studentLst == null)
            {
                _studentLst = new List<Student>();
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

                _studentLst.Add(st1);
                _studentLst.Add(st2);
                _studentLst.Add(st3);
                _studentLst.Add(st4);
                _studentLst.Add(st5);
                _studentLst.Add(st6);

                if (_totakMarkList == null)
                {
                    _totakMarkList = new List<TotalMark>();
                }

                if (_markForStudentList == null)
                {
                    _markForStudentList = new List<MarkForStudent>();
                }

            }
        }
        // GET: StudentController
        public ActionResult Index(int lastestCount = 0)
        {
            //if (lastestCount > 0 && _studentLst.Count >= 5)
            //{
            //    ViewBag.StudentList = _studentLst.OrderByDescending(st => st.Id).ToList().GetRange(0, 5);
            //}
            //else
            //    ViewBag.StudentList = _studentLst;
            if (_studentMarksList == null)
            {
                GetStudentMarkList();
            }
            List<StudentMarkViewModel> studentMarkList = _studentMarksList;
            _studentListViewModel = new StudentListViewModel();
            _studentListViewModel.StudentMarksLst = studentMarkList;
            if (lastestCount > 0 && _studentListViewModel.StudentMarksLst != null && _studentListViewModel.StudentMarksLst.Count >= 5)
            {
                //ViewBag.StudentList = _studentLst.OrderByDescending(st => st.Id).ToList().GetRange(0, 5);
                _studentListViewModel.StudentMarksLst = studentMarkList.OrderByDescending(st => st.StudentId).ToList().GetRange(0, 5);
            }

            return View(_studentListViewModel);
        }

        //Only run 1 time
        public void GetStudentMarkList()
        {

            _studentMarksList = new List<StudentMarkViewModel>();
            foreach (Student st in _studentLst)
            {
                AddTotalMarkViewForNewStudent(st);
            }
        }
        
        /// <summary>
        /// Create new TotalMark and StudentMarkViewModel for new student
        /// </summary>
        /// <param name="student"></param>
        public void AddTotalMarkViewForNewStudent(Student student)
        {
            //Create StudentMarkView
            StudentMarkViewModel stMark = new StudentMarkViewModel();
            stMark.StudentId = student.Id;
            stMark.Name = student.Name;
            stMark.Email = student.Email;
            stMark.StudentAccount = student.StudentAccount;
            stMark.Total = 0;
            stMark.Average = 0;
            stMark.ColorMark = "red";
            _studentMarksList.Add(stMark);

            //Create totalmark
            TotalMark totalMark = new TotalMark();
            totalMark.StudentId = student.Id;
            totalMark.Average = 0;
            totalMark.Total = 0;
            _totakMarkList.Add(totalMark);
        }

        /// <summary>
        /// Search StudentMarkViewModel by searchName and Gender
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="searchGender"></param>
        /// <returns></returns>
        public ActionResult Search(string searchName = "", string searchGender = "")
        {
            List<StudentMarkViewModel> studentMarksList = _studentMarksList;
            if (!String.IsNullOrEmpty(searchName))
            {
                studentMarksList = _studentMarksList.Where(st => st.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!String.IsNullOrEmpty(searchGender))
            {
                bool gender = bool.Parse(searchGender);
                studentMarksList = _studentMarksList.Where(st => st.Gender == gender).ToList();
            }

            StudentListViewModel studentListViewModel = new StudentListViewModel(studentMarksList, searchName: searchName, searchGender: searchGender);
            return View(studentListViewModel);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            Student student = GetStudentById(id);
            ViewBag.Student = student;
            return View();
        }


        // GET: StudentController/Create (Get View)
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

                _studentLst.Add(student);
                AddTotalMarkViewForNewStudent(student);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        //Get view student
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

        //Delete student by studentId
        public ActionResult Delete(int id)
        {
            //Check if student has mark -> not be able to delete
            if (_markForStudentList.Any(st => st.StudentId == id))
            {
                //alert the message...
            }
            else
            {
                Student student = GetStudentById(id);
                _studentLst.Remove(student);
                //remove SudentMarksList
                _studentMarksList.Remove(_studentMarksList.FirstOrDefault(sm => sm.StudentId == id));
                //delete TotalMark
                _totakMarkList.Remove(_totakMarkList.FirstOrDefault(tm => tm.StudentId == id));

            }
            //_studentLst.RemoveAll(st => st.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private Student GetStudentById(int id)
        {
            return _studentLst.FirstOrDefault(st => st.Id == id);
        }

        private bool checkDuplicateEmail(string email, Student? student = null)
        {
            Student getStudent = _studentLst.FirstOrDefault(st => st.Email.Equals(email));

            if (getStudent == null || (student != null && getStudent.Id.Equals(student.Id)))
            {
                return false;
            }

            return true;
        }

        //[ValidateAntiForgeryToken]
        public ActionResult CreateMark(int id)
        {
            Student student = GetStudentById(id);
            if (SubjectController._subjectList == null)
            {
                SubjectController.CreateSubjectList();
            }
            StudentSubjectModel studentSubject = new StudentSubjectModel(id, student.Name, SubjectController._subjectList);
            return View(studentSubject);
        }

        // POST: StudentController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateMark(int studentId, string subjectList, int mark, string studentName)
        {
            if (CheckDuplicateStudentMark(studentId, subjectList))
            {
                ModelState.AddModelError("StudentName", "Student has mark for the subject");
                return RedirectToAction("CreateMark", new { id = studentId, mark = mark });
            }
            //separate method: POST/ GET
            MarkForStudent markForStudent = new MarkForStudent();
            markForStudent.Id = markForStudentCount++;
            markForStudent.StudentId = studentId;
            markForStudent.StudentName = studentName;
            markForStudent.SubjectName = subjectList;
            markForStudent.Mark = mark;
            _markForStudentList.Add(markForStudent);

            //recalculate mark for student
            UpdateTotalMarkNStudentMark(studentId);

            return RedirectToAction(nameof(Index));
        }

        //Get list of marks for all students
        public ActionResult StudentMarkList()
        {
            if (_markForStudentList == null)
            {
                _markForStudentList = new List<MarkForStudent>();
            }
            return View(_markForStudentList);
        }

        public ActionResult DeleteMarkForStudent(int id)
        {
            //Delete record
            MarkForStudent mark4Student = _markForStudentList.FirstOrDefault(m => m.Id == id);
            int studentId = mark4Student.StudentId;
            _markForStudentList.Remove(mark4Student);
            
            UpdateTotalMarkNStudentMark(studentId);

            return RedirectToAction(nameof(StudentMarkList));
        }

        public bool CheckDuplicateStudentMark(int studentId, string subjectName)
        {
            MarkForStudent studentMark = _markForStudentList.FirstOrDefault(st => st.StudentId == studentId && st.SubjectName == subjectName);
            if (studentMark != null) return true;
            return false;
        }

        public bool CheckStudentExist(int studentId)
        {
            bool isExist = (_markForStudentList.FirstOrDefault(st => st.StudentId == studentId) == null) ? false : true;
            return isExist;
        }

        // Add new student mark
        public void UpdateTotalMarkNStudentMark(int studentId)
        {
            TotalMark markObj = CalculateTotalMarkForStudent(studentId);
            UpdateStudentMarkViewModel(studentId, markObj);
        }

        public TotalMark CalculateTotalMarkForStudent(int studentId)
        {
            List<MarkForStudent> stMarkList = _markForStudentList;
            TotalMark markObj = new TotalMark();
            for (int i = 0; i < stMarkList.Count; i++)
            {
                List<MarkForStudent> studentMLlist = stMarkList.FindAll(st => st.StudentId == studentId);
                markObj = _totakMarkList.FirstOrDefault(mark => mark.StudentId == studentId);
                markObj.Total = (studentMLlist != null) ? studentMLlist.Sum(st => st.Mark) : 0;
                markObj.Average = (studentMLlist != null) ? markObj.Total / studentMLlist.Count : 0;
            }
            return markObj;
        }

        public void UpdateStudentMarkViewModel(int studentId, TotalMark markObj)
        {
            StudentMarkViewModel stMark = _studentMarksList.FirstOrDefault(stM => stM.StudentId == studentId);
            stMark.Total = markObj.Total;
            stMark.Average = markObj.Average;
            stMark.ColorMark = markObj.Average > 7 ? "green" : "orange";
        }

    }
}
