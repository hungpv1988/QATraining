// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel.DataAnnotations;

namespace StudentAppMvc.Models.ViewModels
{
    public class StudentsViewModels
    {
        public StudentsViewModels(List<Student> studentList, 
            int top = 5, 
            int page = 1, 
            string searchName = "", 
            string searchGender = "", 
            string searchDOB = "",
            bool isSearchView = false)
        {
            Top = top;
            Page = page;
            SearchName = searchName;
            SearchGender = searchGender;
            SearchDOB = searchDOB;
            IsSearchView = isSearchView;
            StudentList = studentList;
        }

        public int Top { get; set; } = 5;

        public int Page { get; set; } = 1;

        public string SearchName { get; set; } = string.Empty;

        public string SearchGender { get; set; } = string.Empty;

        public string SearchDOB { get; set; } = String.Empty;

        public List<Student> StudentList { get; set; } = new List<Student>();

        public bool IsSearchView { get; set; }
    }
}
