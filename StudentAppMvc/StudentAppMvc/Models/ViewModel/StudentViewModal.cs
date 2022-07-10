using Microsoft.AspNetCore.Mvc.Rendering;
using StudentAppMvc.Data;

namespace StudentAppMvc.Models.ViewModel
{
    public class StudentViewModal : Student
    {
        private readonly ApplicationDbContext? _context;

        public StudentViewModal(int id, string name, DateTime? dateOfBirth, bool gender, string email, string? description, int? classId, string? className): base(id, name, dateOfBirth, gender, email, description, classId, className)
        {

        }
        
        public StudentViewModal(ApplicationDbContext context) => _context = context;

        public StudentViewModal(ApplicationDbContext context, Student student) : base(student) => _context = context;

        List<SelectListItem>? _classes;
        public List<SelectListItem> Classes
        {
            get
            {
                if (_classes == null)
                {
                    List<Class>? classes = _context?.Class?.ToList();
                    SelectListItem firstItem = new SelectListItem() { Value = "", Text = "Select One" };
                    _classes = new SelectList(classes, "Id", "Name").ToList();
                    _classes.Insert(0, firstItem);
                }
                
                return _classes;
            }
        }
    }
}
