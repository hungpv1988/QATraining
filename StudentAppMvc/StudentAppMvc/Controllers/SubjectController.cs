using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;

namespace StudentAppMvc.Controllers
{
    //[AuthenticationFilter]
    [LogginFilter]
    public class SubjectController : Controller
    {
        public static List<Subject> _subjectList;
        private static int count = 1;
        public SubjectController()
        {
            if (_subjectList == null)
            {
                CreateSubjectList();
            }
                
        }

        public static void CreateSubjectList()
        {
                _subjectList = new List<Subject>();
                Subject subject1 = new Subject()
                {
                    Id = count++,
                    Name = "Literature"

                };
                Subject subject2 = new Subject()
                {
                    Id = count++,
                    Name = "English"

                };
                Subject subject3 = new Subject()
                {
                    Id = count++,
                    Name = "Math"

                };

                _subjectList.Add(subject1);
                _subjectList.Add(subject2);
                _subjectList.Add(subject3);
        }
        // GET: StudentController
        public ActionResult Index(int lastestCount = 0)
        {
            return View(_subjectList);
        }


        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {
                subject.Id = count++;
                if (checkDuplicatSubject(subject.Name))
                {
                    ModelState.AddModelError("Name", "Subject is existent!");
                    return View(subject);
                }

                _subjectList.Add(subject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            if (!ThtrStudentController._markForStudentList.Any(st => st.StudentId == id))
            {
                //alert the message...
            }
            else
            {
                Subject subject = GetSubjectById(id);
                _subjectList.Remove(subject);
            }
            //_studentLst.RemoveAll(st => st.Id == id);
            return RedirectToAction(nameof(Index));
        }
       
        public static Subject GetSubjectById(int id)
        {
            return _subjectList.FirstOrDefault(st => st.Id == id);
        }

        private bool checkDuplicatSubject(string subjectName, Subject? subject = null)
        {
            Subject getSubject = _subjectList.FirstOrDefault(st => st.Name.Equals(subjectName));

            if (getSubject == null || (subject != null && getSubject.Id.Equals(subject.Id)))
            {
                return false;
            }

            return true;

        }

        public bool CheckSubjectExist(int subjectId)
        {
            bool isExist = (ThtrStudentController._markForStudentList.FirstOrDefault(st => st.StudentId == subjectId) == null) ? false : true;
            return isExist;
        }
    }
}
