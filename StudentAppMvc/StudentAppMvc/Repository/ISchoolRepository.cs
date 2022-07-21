using StudentAppMvc.Models;

namespace StudentAppMvc.Repository
{
    public interface ISchoolRepository
    {
        Class GetClass(int id);
        List<Class> ListClasses();

        List<Department> ListDepartments();

        Class CreateClass(Class givenClass);

        Class UpdateClass(Class givenClass);
    }
}
