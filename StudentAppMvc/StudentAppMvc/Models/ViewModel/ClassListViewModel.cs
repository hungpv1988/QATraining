using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Models.ViewModel
{
    public class ClassListViewModel
    {
        public List<ClassDto> ClassList { get; set; }

        public string WelcomeMsg { get; set; }
    }
}
