using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Exceptions;
using StudentAppMvc.Filter;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;
using StudentAppMvc.Services;

namespace StudentAppMvc.Controllers
{
    //[AuthenticationFilter]
    //[LogginFilter]
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        public static List<Subject> _subjectList;
        private ISubjectService _subjectService;

        
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // GET: StudentController
        [HttpGet]
        public ActionResult Index()
        {
            SubjectViewModel subjectViewModel = new SubjectViewModel(_subjectService.ListSubject(), "my testing");
            return View(subjectViewModel);
        }


        // GET: StudentController/Create
        [HttpGet]
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
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("Name", "Please input name of subject");
                    return View();
                }

                _subjectService.Create(subject);
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateObjectException doe)
            {
                ModelState.AddModelError("Name", doe.Message);
                return View(subject);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _subjectService.Delete(id);
            }
            catch (ObjectExistInDTOException oe)
            {
                //alert on UI
                Console.WriteLine(oe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
  
             return RedirectToAction(nameof(Index));
        }

        
       
    }
}
