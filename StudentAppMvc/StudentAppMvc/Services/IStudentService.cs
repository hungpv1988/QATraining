using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;

namespace StudentAppMvc.Services
{
    public interface IStudentService
    {
        List<StudentMarkDTO> GetStudentMarkList();
        List<MarkForStudent> GetMarkForStudentList();
        List<StudentMarkDTO> Search(SearchingCriteria searchingCriteria);

        Student GetById(int id);
        void Delete(int id);
        void Create(Student student);
        void Edit(Student student);
        void CreateMark(int studentId, int subjectList, int mark, string studentName);
        void DeleteMarkForStudent(int studentMarkId);
    }
}
