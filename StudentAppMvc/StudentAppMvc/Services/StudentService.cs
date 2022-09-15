using StudentAppMvc.Exceptions;
using StudentAppMvc.Models;
using StudentAppMvc.Models.ViewModels;

namespace StudentAppMvc.Services
{
    public class StudentService : IStudentService
    {
        private static List<Student> _studentLst;
        public static List<StudentMarkDTO> _studentMarksList;
        public static List<MarkForStudent> _markForStudentList;
        private static int markForStudentCount = 1;

        public StudentService()
        {   if(_studentLst == null) CreateNewStudentList();
            if(_studentMarksList == null) GetStudentMarkListAtTheFirstTime();
            if (_markForStudentList == null)
            {
                _markForStudentList = new List<MarkForStudent>();   
            }
        }

        public List<StudentMarkDTO> GetStudentMarkList()
        {
            return _studentMarksList;
        }

        public List<MarkForStudent> GetMarkForStudentList()
        {
            return _markForStudentList;
        }

        public void Create(Student student)
        {
            student.Id = _studentLst.Count + 1;
            if (checkDuplicateEmail(student.Email))
            {
                throw new DuplicateObjectException("Email is existent!");
            }


            _studentLst.Add(student);
            StudentMarkDTOForNewStudent(student);

        }

        public void Delete(int id)
        {
            if (CheckStudentHasMark(id))
            {
                throw new ObjectExistInDTOException("Student has mark, so you cannot delete this student");
            }
            Student student = GetById(id);
            _studentLst.Remove(student);
            //remove SudentMarksList
            _studentMarksList.Remove(_studentMarksList.FirstOrDefault(sm => sm.StudentId == id));

        }

        public void Edit(Student student)
        {
            Student getStudent = GetById(student.Id);
            //if (getStudent == null || (student != null && getStudent.Id.Equals(student.Id)))
            if (checkDuplicateEmail(student.Email) && !getStudent.Id.Equals(student.Id))
            {
                throw new DuplicateObjectException("Email is existent!");
            }

            getStudent.Name = student.Name;
            getStudent.Email = student.Email;
            getStudent.Department = student.Department;
            getStudent.Description = student.Description;
        }

        public Student GetById(int id)
        {
            return _studentLst.FirstOrDefault(st => st.Id == id);
        }
 
        public void DeleteMarkForStudent(int studentMarkId)
        {
            //Delete record
            MarkForStudent mark4Student = _markForStudentList.FirstOrDefault(m => m.Id == studentMarkId);
            int studentId = mark4Student.StudentId;
            _markForStudentList.Remove(mark4Student);

            UpdateTotalMarkNStudentMark(studentId);
        }

        public List<StudentMarkDTO> Search(string searchName = "", string searchGender = "")
        {
            List<StudentMarkDTO> studentMarksList = _studentMarksList;
            if (!String.IsNullOrEmpty(searchName))
            {
                studentMarksList = _studentMarksList.Where(st => st.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!String.IsNullOrEmpty(searchGender))
            {
                bool gender = bool.Parse(searchGender);
                studentMarksList = _studentMarksList.Where(st => st.Gender == gender).ToList();
            }

            return studentMarksList;
        }

        public void CreateMark(int studentId, int subjectList, int mark, string studentName)
        {
            if (CheckDuplicateStudentMark(studentId, subjectList))
            {
                throw new DuplicateObjectException("Student has mark for this subject");
            }
            //separate method: POST/ GET
            MarkForStudent markForStudent = new MarkForStudent();
            markForStudent.Id = markForStudentCount++;
            markForStudent.StudentId = studentId;
            markForStudent.StudentName = studentName;
            markForStudent.SubjectId = subjectList;
            markForStudent.SubjectName = studentName;
            markForStudent.Mark = mark;
            _markForStudentList.Add(markForStudent);

            //recalculate mark for student
            UpdateTotalMarkNStudentMark(studentId);
        }

        private void CreateNewStudentList()
        {
            _studentLst = new List<Student>()
            {
                new Student()
            {
                Id = 1,
                Name = "Thu",
                Description = "desc",
                Department = "dep1",
                Email = "thu@ptit.edu"
            },
                new Student()
            {
                Id = 2,
                Name = "Trang",
                Description = "Trang desc",
                Department = "dep2",
                Email = "trang@ptit.edu"
            },
                new Student()
            {
                Id = 3,
                Name = "Duong",
                Description = "Duong desc",
                Department = "dep2",
                Email = "duong@ptit.edu"
            },
                new Student()
            {
                Id = 4,
                Name = "Nga",
                Description = "Nga desc",
                Department = "dep3",
                Email = "Nga@ptit.edu"
            },

            new Student()
            {
                Id = 5,
                Name = "Mai",
                Description = "Mai desc",
                Department = "dep3",
                Email = "Mai@ptit.edu"
            },

            new Student()
            {
                Id = 6,
                Name = "Hoa",
                Description = "Hoa desc",
                Department = "dep3",
                Email = "Hoa@ptit.edu",
                StudentAccount = "9092213123"

            }
            };
        }

        private void GetStudentMarkListAtTheFirstTime()
        {
            _studentMarksList = new List<StudentMarkDTO>();
            foreach (Student st in _studentLst)
            {
                StudentMarkDTOForNewStudent(st);
            }
        }

        private void StudentMarkDTOForNewStudent(Student student)
        {
            //Create StudentMarkView
            StudentMarkDTO stMark = new StudentMarkDTO();
            stMark.StudentId = student.Id;
            stMark.Name = student.Name;
            stMark.Email = student.Email;
            stMark.StudentAccount = student.StudentAccount;
            stMark.Total = 0;
            stMark.Average = 0;
            stMark.ColorMark = "red";

            _studentMarksList.Add(stMark);
        }



        private bool CheckDuplicateStudentMark(int studentId, int subjectId)
        {
            MarkForStudent studentMark = _markForStudentList.FirstOrDefault(st => st.StudentId == studentId && st.SubjectId == subjectId);
            if (studentMark != null) return true;
            return false;
        }


        private void UpdateTotalMarkNStudentMark(int studentId)
        {
            List<MarkForStudent> markForOneStudentList = _markForStudentList.FindAll(st => st.StudentId == studentId);
            StudentMarkDTO studentMark = _studentMarksList.FirstOrDefault(stm => stm.StudentId == studentId);
            studentMark.Total = (markForOneStudentList != null) ? markForOneStudentList.Sum(st => st.Mark) : 0;
            studentMark.Average = (markForOneStudentList != null) ? studentMark.Total / markForOneStudentList.Count : 0;
            studentMark.ColorMark = studentMark.Average > 7 ? "green" : "orange";
        }




        private bool checkDuplicateEmail(string email)
        {
            Student getStudent = _studentLst.FirstOrDefault(st => st.Email.Equals(email));

            //if (getStudent == null || (student != null && getStudent.Id.Equals(student.Id)))
            if (getStudent == null)
            {
                return false;
            }

            return true;
        }
        private bool CheckStudentHasMark(int studentId)
        {
            if (_markForStudentList.Any(st => st.StudentId == studentId))
            {
                return true;
            }
            return false;
        }
    }
}

