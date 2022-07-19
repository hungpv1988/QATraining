namespace StudentAppMvc.Models.ViewModels
{
    public class StudentListViewModel
    {
        public StudentListViewModel()
        { 
        }

        public StudentListViewModel(List<StudentMarkViewModel> studentMarkList, string searchName, string searchGender)
        {
            StudentMarksLst = studentMarkList;
            SearchName = searchName;
            SearchGender = searchGender;
        }


        public List<StudentMarkViewModel> StudentMarksLst { get; set; }
        public string SearchName { get; set; }
        public string SearchGender { get; set; }
        

    }
}
