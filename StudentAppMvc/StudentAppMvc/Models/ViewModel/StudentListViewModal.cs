namespace StudentAppMvc.Models.ViewModel
{
    public class StudentListViewModal
    {
        public StudentListViewModal(List<Student> students, int totalPage, int currentPage, string searchString, string nameSortParm)
        {
            Students = students;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            SearchString = searchString;
            NameSortOrder = nameSortParm;
        }

        public List<Student> Students { get; set; }

        public int TotalPage { get; set; }

        public int CurrentPage { get; set; }

        public string SearchString { get; set; }

        public string NameSortOrder { get; set; }
    }
}
