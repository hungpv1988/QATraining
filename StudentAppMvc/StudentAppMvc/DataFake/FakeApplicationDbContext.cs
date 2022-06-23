using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Models;

namespace StudentAppMvc.DataFake
{
    public interface IAppDbContext
    {
        public DbSet<Student> Student { get; set; }
        void SaveChangesAsync();
        void Add(Student student);

        void Update(Student student);

    }

    public class FakeApplicationDbContext : IAppDbContext
    {

        DbSet<Student> _Students;
        DbSet<Student> IAppDbContext.Student {
            get { return _Students; }

            set { _Students = value; }
        }

        void IAppDbContext.Add(Student student)
        {
            throw new NotImplementedException();
        }

        void IAppDbContext.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        void IAppDbContext.Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}