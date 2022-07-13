using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAppMvc.Data;

namespace StudentAppMvc.Models.ViewModel
{
    public class StudentListViewModal
    {
        public static string ALL_CLASS_ID = "-1";
        public static string NO_CLASS_ID = "-2";

        private readonly ApplicationDbContext? _context;

        public StudentListViewModal(List<Student> students, int totalPage, int currentPage, string searchString, string nameSortParm, int? classId, ApplicationDbContext? context)
        {
            Students = students;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            SearchString = searchString;
            NameSortOrder = nameSortParm;
            ClassId = classId;
            _context = context;
        }

        public List<Student> Students { get; set; }

        public int TotalPage { get; set; }

        public int CurrentPage { get; set; }

        public string SearchString { get; set; }

        public string NameSortOrder { get; set; }

        public int? ClassId { get; set; }

        List<SelectListItem>? _classes;
        public List<SelectListItem> Classes
        {
            get
            {
                if (_classes == null)
                {
                    List<Class>? classes = _context?.Class?.ToList();
                    SelectListItem allClasses = new SelectListItem() { Value = StudentListViewModal.ALL_CLASS_ID, Text = "All Classes" };
                    SelectListItem noneClasses = new SelectListItem() { Value = StudentListViewModal.NO_CLASS_ID, Text = "No classes yet" };
                    _classes = new SelectList(classes, "Id", "Name").ToList();
                    _classes.Insert(0, noneClasses);
                    _classes.Insert(0, allClasses);
                }

                return _classes;
            }
        }
    }
}
