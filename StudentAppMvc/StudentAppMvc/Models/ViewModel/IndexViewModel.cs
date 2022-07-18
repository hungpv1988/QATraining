using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Models.ViewModel
{
    public class IndexViewModel
    {
        public List<ClassDto> ClassList { get; set; }

        public string WelcomeMsg { get; set; }
    }
}
