using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAppMvc.Models;

namespace StudentAppMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentAppMvc.Models.Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().ToTable("Student");

            builder.Entity<Class>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Classes)
                .HasForeignKey(c => c.DepartmentCode);

            base.OnModelCreating(builder);
        }

        public DbSet<StudentAppMvc.Models.Department>? Department { get; set; }

        public DbSet<StudentAppMvc.Models.Class>? Class { get; set; }
    }
}