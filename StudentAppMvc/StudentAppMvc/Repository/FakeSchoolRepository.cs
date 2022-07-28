using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Data;
using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Repository
{
    // default repo should work with Sql DB
    public class FakeSchoolRepository : ISchoolRepository
    {
        List<Class> _classes = new List<Class>();
        List<Department> _departments = new List<Department>();
        
        public FakeSchoolRepository() 
        {
            if (_classes.Count == 0)
            {
                _classes.Add(new Class(1, "Info 01", "Information"));
                _classes.Add(new Class(2, "Chemical 01", "Chemical"));
                _classes.Add(new Class(3, "Electric 01", "Electric"));
                _classes.Add(new Class(4, "Electric 02", "Electric"));
            }

            if (_departments.Count == 0)
            {
                _departments.Add(new Department("Information", "Information", "Information science"));
                _departments.Add(new Department("Chemical", "Chemical", "Chemical Sciences"));
                _departments.Add(new Department("Electric", "Electric", "Faculty of Electrical Engineering"));
            }


        }

        public Class GetClass(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }

            return _classes.FirstOrDefault(x => x.Id == id);
        }

        public Class CreateClass(Class givenClass)
        {
            var isClassExistedBefore = _classes.Any(x => x.Name == givenClass.Name);


            if (isClassExistedBefore) 
            {
                throw new Exception();
            }

            givenClass.Id = _classes.Count + 1;
            _classes.Add(givenClass);

            return givenClass;
        }

        public Class UpdateClass(Class givenClass)
        {
            var index = _classes.FindIndex(c => c.Id == givenClass.Id);

            if (index >= 0)
            {
                _classes[index] = givenClass;
            }

            return givenClass;
        }

        public List<Class> ListClasses()
        {
            return _classes.ToList();
        }

        public Department GetDepartment(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentException();
            }

            return _departments.FirstOrDefault(x => x.Code == code);
        }

        public Department CreateDepartment(Department givenDepartment)
        {
            var isExist = _departments.Any(x => x.Code == givenDepartment.Code);


            if (isExist)
            {
                throw new Exception();
            }

            _departments.Add(givenDepartment);

            return givenDepartment;
        }

        public Department UpdateDepartment(Department givenDepartment)
        {
            var index = _departments.FindIndex(c => c.Code == givenDepartment.Code);

            if (index >= 0)
            {
                _departments[index] = givenDepartment;
            }

            return givenDepartment;
        }

        public List<Department> ListDepartments()
        {
            return _departments.ToList();
        }

        public Class DeleteClass(int id)
        {
            var index = _classes.FindIndex(c => c.Id == id);
            Class deletedClass;

            if (index >= 0)
            {
                deletedClass = _classes[index];
                _classes.RemoveAt(index);
            }
            else
            {
                throw new Exception();
            }

            return deletedClass;
        }

        public Department DeleteDepartment(string code)
        {
            var index = _departments.FindIndex(c => c.Code == code);
            Department deletedDepartment;

            if (index >= 0)
            {
                deletedDepartment = _departments[index];
                _departments.RemoveAt(index);
            }
            else
            {
                throw new Exception();
            }

            return deletedDepartment;
        }
    }
}
