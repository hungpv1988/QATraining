using Microsoft.EntityFrameworkCore;
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

        public Class GetClass(int id)
        {
            if (_context.Class == null || id <= 0)
            {
                throw new ArgumentException();
            }

            return _context.Class.FirstOrDefault(x => x.Id == id);
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

        public Class UpdateClass(Class givenClass)
        {
            if (_context.Class == null)
            {
                throw new ArgumentException();
            }

            try
            {
                _context.Class.Update(givenClass);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return givenClass;
        }

        public Class DeleteClass(int id)
        {
            if (_context.Class == null)
            {
                throw new ArgumentException();
            }

            Class _deleteClass;

            try { 
                _deleteClass = GetClass(id);
                _context.Class.Remove(_deleteClass);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return _deleteClass;
        }

        public List<Class> ListClasses()
        {
            if (_context.Class == null) 
            {
                return null;
            }

            return _context.Class.ToList();
        }

        public Department GetDepartment(String code)
        {
            if (_context.Department == null || String.IsNullOrEmpty(code))
            {
                throw new ArgumentException();
            }

            return _context.Department.FirstOrDefault(x => x.Code == code);
        }

        public Department CreateDepartment(Department givenDepartment)
        {
            if (_context.Department == null)
            {
                throw new ArgumentException();
            }

            var isDepartmentExistedBefore = _context.Department.Any(x => x.Name == givenDepartment.Name || x.Code == givenDepartment.Code);

            if (isDepartmentExistedBefore)
            {
                throw new Exception();
            }

            _context.Department.Add(givenDepartment);
            _context.SaveChanges();

            return givenDepartment;
        }

        public Department UpdateDepartment(Department givenDepartment)
        {
            if (_context.Department == null)
            {
                throw new ArgumentException();
            }

            try
            {
                _context.Department.Update(givenDepartment);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return givenDepartment;
        }

        public List<Department> ListDepartments()
        {
            if (_context.Department == null)
            {
                return null;
            }

            return _context.Department.ToList();
        }

        public Department DeleteDepartment(string code)
        {
            if (_context.Department == null)
            {
                throw new ArgumentException();
            }

            Department _deleteDepartment;

            try
            {
                _deleteDepartment = GetDepartment(code);
                _context.Department.Remove(_deleteDepartment);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return _deleteDepartment;
        }
    }
}
