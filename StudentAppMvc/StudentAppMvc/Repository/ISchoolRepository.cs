using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Repository
{
    public interface ISchoolRepository
    {
        Class GetClass(int id);

        Class CreateClass(Class givenClass);

        Class UpdateClass(Class givenClass);

        Class DeleteClass(int id);

        List<Class> ListClasses();

        Department GetDepartment(String code);

        Department CreateDepartment(Department givenDepartment);

        Department UpdateDepartment(Department givenDepartment);

        Department DeleteDepartment(string code);
        
        List<Department> ListDepartments();

    }
}
