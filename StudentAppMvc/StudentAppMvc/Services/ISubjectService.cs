using StudentAppMvc.Models;

namespace StudentAppMvc.Services
{
    public interface ISubjectService
    {
        List<Subject> ListSubject();
        Subject Get(int id);
        void Delete(int id);
        void Create(Subject subject);

    }
}
