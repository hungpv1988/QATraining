using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Exceptions;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;
using StudentAppMvc.Services;

namespace StudentAppMvc.Controllers
{
    //[AuthenticationFilter]
    [LogginFilter]

    public class ThtrStudentController : Controller
    {
        private static StudentListViewModel _studentListViewModel;

        private ISubjectService _subjectService;
        private IStudentService _studentService;
        private ValidationService _validationService;
        public ThtrStudentController(IStudentService studentService, ISubjectService subjectService, ValidationService validationService)
        {
            _subjectService = subjectService;
            _studentService = studentService;
            _validationService = validationService;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            List<StudentMarkDTO> studentMarkList = _studentService.GetStudentMarkList();
            //if (lastestCount > 0 && studentMarkList != null && studentMarkList.Count >= 5)
            //{
            //    studentMarkList = studentMarkList.OrderByDescending(st => st.StudentId).ToList().GetRange(0, 5);
            //}

            return View(new StudentListViewModel(studentMarkList));
        }



        /// <summary>
        /// Search StudentMarkViewModel by searchName and Gender
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="searchGender"></param>
        /// <returns></returns>
        public ActionResult Search(SearchingCriteria searchingCriteria)
        {
            StudentListViewModel studentListDTO = new StudentListViewModel(_studentService.Search(searchingCriteria));
            ViewBag.SearchingCriteria = searchingCriteria;
            return View(studentListDTO);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            Student student = _studentService.GetById(id);
            ViewBag.Student = student;
            return View(student);
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
                var result = _validationService.Validate(student);
                if (result.Any())
                {
                    throw new Exception("error");
                }
                _studentService.Create(student);
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateObjectException doe)
            {
                ModelState.AddModelError("Email", doe.Message);
                return View(student);
            }
            catch (Exception)
            {
                return View();
            }
        }

        //Get view student
        public ActionResult Edit(int id)
        {
            Student student = _studentService.GetById(id);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Student student)
        public ActionResult Edit(Student student)
        {
            try
            {
                _studentService.Edit(student);
            }
            catch (DuplicateObjectException doe)
            {
                ModelState.AddModelError("Email", doe.Message);
                return View(student);
            }
            catch (Exception)
            {
                return View(student);
            }

            return RedirectToAction(nameof(Index));
            
        }

        //Delete student by studentId
        public ActionResult Delete(int id)
        {
            try 
            {
                _studentService.Delete(id);
            }
            catch(ObjectExistInDTOException oe)
            {
                Console.WriteLine(oe.Message);

            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        //[ValidateAntiForgeryToken]
        public ActionResult CreateMark(int id)
        {
            Student student = _studentService.GetById(id);
            StudentSubjectModel studentSubject = new StudentSubjectModel(id, student.Name, _subjectService.ListSubject());
            return View(studentSubject);
        }

        // POST: StudentController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateMark(int studentId, int subjectList, int mark, string studentName)
        {
            try
            {
                _studentService.CreateMark(studentId, subjectList, mark, studentName);
            }
            catch (DuplicateObjectException doe)
            {
                ModelState.AddModelError("StudentName", doe.Message);
                return RedirectToAction("CreateMark", new { id = studentId, mark = mark });
            }
            catch (Exception e)
            {
                Console.WriteLine("-----msg:" + e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        //Get list of marks for all students
        public ActionResult StudentMarkList()
        {
            return View(_studentService.GetMarkForStudentList());
        }

        public ActionResult DeleteMarkForStudent(int id)
        {
            //Delete record
            _studentService.DeleteMarkForStudent(id);
            return RedirectToAction(nameof(StudentMarkList));
        }

    }
}
