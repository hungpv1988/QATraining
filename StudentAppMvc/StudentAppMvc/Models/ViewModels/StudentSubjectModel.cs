namespace StudentAppMvc.Models.ViewModels
{
    public class StudentSubjectModel
    {
        public StudentSubjectModel(int studentId, string studentName, List<Subject> subjectList)
        {
            StudentId = studentId;
            StudentName = studentName;
            SubjectList = subjectList;
        }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Subject> SubjectList;
        public int Mark { get; set; }
    }
}
