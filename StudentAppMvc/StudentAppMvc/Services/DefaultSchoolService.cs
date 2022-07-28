using StudentAppMvc.ExtensionMethods;
using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;
using StudentAppMvc.Models.ViewModel;
using StudentAppMvc.Repository;

namespace StudentAppMvc.Services
{
    public class DefaultSchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly string _cacheKeyClassList = "classList";
        private readonly string _cacheKeyDepartmentList = "departmentList";
        private static IDictionary<string, object> _cache;
        private readonly string _cacheKeyClassId = "classid{0}";
        private readonly string _cacheKeyDepartmentId = "department{0}";

        public DefaultSchoolService(ISchoolRepository schoolRepository) 
        {
            _schoolRepository = schoolRepository;
            _cache = new Dictionary<string, object>();
        }

        public ClassDto GetClass(int id)
        {
            var key = string.Format(_cacheKeyClassId, id);
            if (_cache.ContainsKey(key)) 
            {
                return _cache[key] as ClassDto;
            }

            var givenClass = _schoolRepository.GetClass(id);

            if (givenClass == null) 
            {
                return null;
            }
            _cache[key] = givenClass.ToClassDto();
            return _cache[key] as ClassDto;
        }

        public ClassDto AddClass(ClassDto classDto)
        {
            // do validation here, add more for open for exten and close for modification

            var givenClass = new Class()
            {
                Name = classDto.Name,
                DepartmentCode = classDto.DepartmentCode
            };

            givenClass =  _schoolRepository.CreateClass(givenClass);
            var key = string.Format(_cacheKeyClassId, givenClass.Id);
            _cache[key] = givenClass.ToClassDto();

            return _cache[key] as ClassDto;
        }

        public ClassDto UpdateClass(ClassDto classDto)
        {
            var updatedClass = _schoolRepository.UpdateClass(new Class() { 
                Id = classDto.Id,
                Name = classDto.Name,
                DepartmentCode= classDto.DepartmentCode
            });
            var key = string.Format(_cacheKeyClassId, updatedClass.Id);
            _cache[key] = updatedClass.ToClassDto();

            return _cache[key] as ClassDto;
        }

        public List<ClassDto> ListClasses()
        {
            if (_cache.ContainsKey(_cacheKeyClassList))
            {
                return _cache[_cacheKeyClassList] as List<ClassDto>;
            }

            var classList = _schoolRepository.ListClasses();
            if (classList == null || classList.Count == 0)
            {
                return null;
            }

            _cache[_cacheKeyClassList] = classList.Select(c => ConvertClassToClassDto(c)).ToList();
            return _cache[_cacheKeyClassList] as List<ClassDto>;
        }

        public ClassDto DeleteClass(int id)
        {
            ClassDto deletedClass =  _schoolRepository.DeleteClass(id).ToClassDto();
            
            var key = string.Format(_cacheKeyClassId, id);
            _cache.Remove(key);

            return deletedClass;
        }

        private ClassDto ConvertClassToClassDto(Class givenClass) 
        {
            var classDto = givenClass.ToClassDto();
            var department = ListDepartments(); // has been cached, so should be fine.
            var associatedDep = department.FirstOrDefault(d => d.Code == givenClass.DepartmentCode);
            if (associatedDep != null) 
            {
                classDto.DepartmentName = associatedDep.Name;
            }

            return classDto;
        }

        public DepartmentDto GetDepartment(string code)
        {
            var foundDepartment = _schoolRepository.GetDepartment(code);

            if (foundDepartment == null)
            {
                return null;
            }
            
            return foundDepartment.ToDepartmentDto();
        }

        public DepartmentDto AddDepartment(DepartmentDto departmentDto)
        {
            var _department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            _department = _schoolRepository.CreateDepartment(_department);

            return _department.ToDepartmentDto();
        }

        public DepartmentDto UpdateDepartment(DepartmentDto departmentDto)
        {
            var updatedDepartment = _schoolRepository.UpdateDepartment(new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description
            });

            return updatedDepartment.ToDepartmentDto();
        }

        public List<DepartmentDto> ListDepartments()
        { 
            var departmentList = _schoolRepository.ListDepartments();
            if (departmentList == null || departmentList.Count == 0)
            {
                return null;
            }

            return departmentList.Select(d => d.ToDepartmentDto()).ToList();
        }

        public DepartmentDto DeleteDepartment(string code)
        {
            return _schoolRepository.DeleteDepartment(code).ToDepartmentDto();
        }

    }
}
