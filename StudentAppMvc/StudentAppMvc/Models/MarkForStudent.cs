namespace StudentAppMvc.Models
{
    public class MarkForStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int  SubjectId { get; set; }
        public int Mark { get; set; }
    }
}
