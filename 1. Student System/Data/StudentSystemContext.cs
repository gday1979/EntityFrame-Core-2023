using _1._Student_System.Data.Models;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data 
{
    public class StudentSystemContext : DbContext

    {
        public StudentSystemContext()
        {
            
        }
        public StudentSystemContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<Homework> Homeworks { get; set; } = null!;

        public  DbSet<Course> Courses { get; set; } = null!;

        public  DbSet<Resource> Resources { get; set; }=null!;

        public DbSet<StudentCourse> StudentsCourses { get; set; } = null!; 
        public  DbSet<Student>Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                
            }); ;
              
                
        }

    }
}
