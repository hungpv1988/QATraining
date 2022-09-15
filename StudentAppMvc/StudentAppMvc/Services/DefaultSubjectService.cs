using StudentAppMvc.Controllers;
using StudentAppMvc.Exceptions;
using StudentAppMvc.Models;
using StudentAppMvc.Repository;

namespace StudentAppMvc.Services
{
    public class DefaultSubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public DefaultSubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public List<Subject> ListSubject()
        {
            return _subjectRepository.GetListSubject();
        }

        public Subject Get(int id)
        {
            return _subjectRepository.GetById(id);

        }
        public void Delete(int id)
        {
            try
            {
                _subjectRepository.Delete(id);
            }
            catch (ObjectExistInDTOException oe)
            {
                throw new ObjectExistInDTOException(oe.Message);
            }
            catch (Exception)
            {
                throw new Exception("System error");
            }

        }


        public void Create(Subject subject)
        {
            try
            {
                _subjectRepository.Create(subject);
            }
            catch (DuplicateObjectException doe)
            {
                throw new DuplicateObjectException(doe.Message);
            }
            catch (Exception)
            {
                throw new Exception("System error");
            }
        }
    }
}
