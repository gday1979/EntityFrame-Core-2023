using _1._Student_System.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 	P01_StudentSystem.Data.Models 
{
    public class Student
    {
        public Student()
        {
            this.StudentsCourses=new HashSet<StudentCourse>(); 
           this.Homeworks=new HashSet<Homework>();
        }
        public int StudentId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(10)]
        public string? PhoneNumber { get; set; } 

        public DateTime RegisteredOn { get; set; }

        public DateTime ?Birthday { get; set; }

        public ICollection<StudentCourse> StudentsCourses { get; set; } = null!;

        public ICollection<Homework>Homeworks { get; set; }=null!;

    }
}
