namespace StudentAppMvc.Models.ViewModels
{
    public class StudentListViewModel
    {
        public StudentListViewModel()
        { 
        }

        public StudentListViewModel(List<StudentMarkDTO> studentMarkList, string searchName = "", string searchGender = "")
        {
            StudentMarksLst = studentMarkList;
            SearchName = searchName;
            SearchGender = searchGender;
        }

        public List<StudentMarkDTO> StudentMarksLst { get; set; }
        public string SearchName { get; set; }
        public string SearchGender { get; set; }
        

    }
}
