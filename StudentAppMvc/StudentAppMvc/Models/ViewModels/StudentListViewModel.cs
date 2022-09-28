namespace StudentAppMvc.Models.ViewModels
{
    public class StudentListViewModel
    {
        public StudentListViewModel()
        { 
        }

        public StudentListViewModel(List<StudentMarkDTO> studentMarkList)
        {
            StudentMarksLst = studentMarkList;
        }

        public List<StudentMarkDTO> StudentMarksLst { get; set; }

    }
}
