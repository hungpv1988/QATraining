using StudentAppMvc.Models;

namespace StudentAppMvc.Repository
{
    public interface ISubjectRepository
    {
        List<Subject> GetListSubject();

        Subject GetById(int id);

        void Delete(int id);

        Subject GetByName(string subjectName);

        void Create(Subject subject);
    }
}
