using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Models;

namespace StudentAppMvc.Data
{
    public class MyData 
    {
        public static List<Student> Students = new List<Student>();
        public static List<Teacher> Teachers = new List<Teacher>();
        public static List<Subject> Subjects = new List<Subject>();

        static MyData()
        {
            if(Students.Count == 0)
            {
                Students.Add(new Student(Students.Count + 1, "Minh", "Sample student", DateTime.Now, false, "minh@hut.edu"));
                Students.Add(new Student(Students.Count + 1, "Bảo Minh", "Sample student", DateTime.Now.Subtract(TimeSpan.FromDays(1000)), true, "bao@hut.edu"));
                Students.Add(new Student(Students.Count + 1, "Ngọc Minh", "Sample student", DateTime.Now.Subtract(TimeSpan.FromDays(700)), false, "ngoc@hut.edu"));
                Students.Add(new Student(Students.Count + 1, "Kang minh", "Sample student", DateTime.Now.Subtract(TimeSpan.FromDays(300)), true, "kang@hut.edu"));
                Students.Add(new Student(Students.Count + 1, "Khanh", "Sample student", DateTime.Now, false, "khanh@hut.edu"));
                Students.Add(new Student(Students.Count + 1, "Hi", "Sample student", DateTime.Now, false, "hi@hut.edu"));
            }
            if (Subjects.Count == 0)
            {
                Subjects.Add(new Subject(Subjects.Count + 1, "Tính toán khoa học", "Tính toán khoa học"));
                Subjects.Add(new Subject(Subjects.Count + 1, "Điện tử số", "Điện tử số"));
                Subjects.Add(new Subject(Subjects.Count + 1, "Vi xử lý", "Vi xử lý"));
                Subjects.Add(new Subject(Subjects.Count + 1, "Trí tuệ nhân tạo", "Trí tuệ nhân tạo"));
                Subjects.Add(new Subject(Subjects.Count + 1, "Xử lý ảnh", "Xử lý ảnh"));
                Subjects.Add(new Subject(Subjects.Count + 1, "Lập trình hướng đối tượng", "Lập trình hướng đối tượng"));
            }
            if (Teachers.Count == 0)
            {
                Teachers.Add(new Teacher(Teachers.Count + 1, "Nguyễn Đức Nghĩa", "KHMT"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Trịnh Văn Loan", "KHMT"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Văn Thế Minh", "KHMT"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Nguyễn Thanh Thủy", "HTTT"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Nguyễn Linh Giang", "MTT"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Huỳnh Quyết Thắng", "CNPM"));
                Teachers.Add(new Teacher(Teachers.Count + 1, "Đỗ Văn Uy", "HTTT"));
            }
        }
    }
}