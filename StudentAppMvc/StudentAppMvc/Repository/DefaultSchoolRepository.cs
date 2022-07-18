using StudentAppMvc.Data;
using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Repository
{
    // default repo should work with Sql DB
    public class DefaultSchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext _context;
        
        public DefaultSchoolRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Class CreateClass(Class givenClass)
        {
            if (_context.Class == null)
            {
                throw new ArgumentException();
            }

            var isClassExistedBefore = _context.Class.Any(x => x.Name == givenClass.Name);

            var query = _context.Class.Where(x => x.Name == givenClass.Name);
            //var sql = query.ToQueryS();


            if (isClassExistedBefore) 
            {
                throw new Exception();
            }

            _context.Class.Add(givenClass);
            _context.SaveChanges();
            return givenClass;
        }

        public Class Get(int id)
        {
            if (_context.Class == null || id <= 0) 
            {
                throw new ArgumentException();
            }

            return _context.Class.FirstOrDefault(x => x.Id == id);
        }

        public List<Class> ListClasses()
        {
            if (_context.Class == null) 
            {
                return null;
            }

            return _context.Class.ToList();
        }

        public List<Department> ListDepartments()
        {
            if (_context.Department == null)
            {
                return null;
            }

            return _context.Department.ToList();
        }
    }
}
