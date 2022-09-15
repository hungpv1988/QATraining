using StudentAppMvc.Exceptions;
using StudentAppMvc.Models;
using StudentAppMvc.Services;

namespace StudentAppMvc.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        public static List<Subject> _subjectList;
        //private ISubjectRepository _subjectlRepository;
        private IStudentService _studentService;
        public SubjectRepository(IStudentService studentService)
        {
            if (_subjectList == null)
            {
                CreateSubjectList();
            }
  
            _studentService = studentService;
        }


        private void CreateSubjectList()
        {
            _subjectList = new List<Subject>()
            {
                new Subject()
                {
                Id = 1,
                Name = "Literature"

                 },
                new Subject()
            {
                Id = 2,
                Name = "English"

            },
                new Subject()
            {
                Id = 3,
                Name = "Math"

            }

            };

        }

        public void Delete(int id)
        {
            if (CheckSubjectExistInStudentMark(id))
            {
                throw new ObjectExistInDTOException("Students have marks in this subject");
            }
            else
            {
                Subject subject = GetById(id);
                _subjectList.Remove(subject);
            }
        }

        public Subject GetById(int id)
        {
            return _subjectList.FirstOrDefault(subj => subj.Id == id);
        }

        public List<Subject> GetListSubject()
        {
            return _subjectList;
        }

        public void Create(Subject subject)
        {
            if (GetByName(subject.Name) != null)
            {
                throw new DuplicateObjectException("Subject is existent!");
            }

            subject.Id = _subjectList.Count + 1;
            _subjectList.Add(subject);
        }

        public Subject GetByName(string subjectName)
        {
            return _subjectList.FirstOrDefault(subj => string.Equals(subj.Name, subjectName, StringComparison.OrdinalIgnoreCase));
        }

        private bool CheckSubjectExistInStudentMark(int subjectId)
        {
            bool isExist = (_studentService.GetMarkForStudentList().FirstOrDefault(subj => subj.SubjectId == subjectId) == null) ? false : true;
            return isExist;
        }
    }
}
