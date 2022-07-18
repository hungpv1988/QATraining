using StudentAppMvc.Models;

namespace StudentAppMvc.Repository
{
    public interface ISchoolRepository
    {
        Class Get(int id);
        List<Class> ListClasses();

        List<Department> ListDepartments();

        Class CreateClass(Class givenClass);
    }
}
