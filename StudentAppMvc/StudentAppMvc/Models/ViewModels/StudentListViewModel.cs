namespace StudentAppMvc.Models.ViewModels
{
    public class StudentListViewModel
    {
        public StudentListViewModel()
        { 
        }

        public StudentListViewModel(List<StudentMarkDTO> studentMarkList, SearchingCriteria? searchingCritera = null)
        {
            StudentMarksLst = studentMarkList;
        }
        
        public SearchingCriteria SearchingCriteriaLst { get; set; }
        public List<StudentMarkDTO> StudentMarksLst { get; set; }

    }
}
