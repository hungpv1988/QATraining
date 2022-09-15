namespace StudentAppMvc.Models.ViewModels
{
    public class SubjectViewModel
    {

        public SubjectViewModel(List<Subject> subjectList, string message)
        {
            SubjectList = subjectList;
            myMessage = message;
        }
        public List<Subject> SubjectList { get; set; }
        public string myMessage { get; set; }

    }
}
