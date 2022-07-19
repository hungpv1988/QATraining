namespace StudentAppMvc.Models.ViewModels
{
    public class StudentMarkViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set;}
        public string Email { get; set; }
        public string StudentAccount { get; set; }
        public bool Gender { get; set; }
        public int Total { get; set; }
        public float Average { get; set; }
        public string ColorMark { get; set; }
    }
}
