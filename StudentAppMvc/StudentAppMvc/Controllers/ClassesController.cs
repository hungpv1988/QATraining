using Microsoft.AspNetCore.Mvc;
using StudentAppMvc.Filter;
using StudentAppMvc.Models.DTO;
using StudentAppMvc.Models.ViewModel;
using StudentAppMvc.Services;

namespace StudentAppMvc.Controllers
{
    [AuthenticationFilter]
    public class ClassesController : Controller
    {
        private ISchoolService _schoolService;

        public ClassesController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var classList = _schoolService.ListClasses();
            var indexViewModel = new IndexViewModel()
            {
                ClassList = classList,
                WelcomeMsg = "Welcome to all classes in the year of 2022"
            };

            return View(indexViewModel);
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _schoolService.Get(id.Value);
            return View(viewModel);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ClassCreationViewModel viewModel = new ClassCreationViewModel()
            {
                Departments = _schoolService.ListDepartments() ?? new List<DepartmentDto>(),
                UpcomingPeriod = "The next period is from Step 2022 to 15th Oct 2022"
            };

            
            return View(viewModel);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DepartmentCode")] ClassCreationViewModel @classViewModel)
        {
            if (ModelState.IsValid)
            {
                var givenClass = new ClassDto()
                {
                    DepartmentCode = @classViewModel.DepartmentCode,
                    Name = @classViewModel.Name
                };

                givenClass = _schoolService.AddClass(givenClass);
                return RedirectToAction(nameof(Index));
            }

            ClassCreationViewModel viewModel = new ClassCreationViewModel()
            {
                Departments = _schoolService.ListDepartments(),
                UpcomingPeriod = "The next period is from Step 2022 to 15th Oct 2022"
            };

            return View(@classViewModel);
        }

        //private bool ClassExists(int id)
        //{
        //  return (_context.Class?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
