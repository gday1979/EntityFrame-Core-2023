using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models 
{
    public class Homework
    {
        public int HomeworkId { get; set; }
        [Required]
        public string? Content { get; set; } 

        public ContentType ContetType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }

        public Student ?Student { get; set; }

        public int CourseId { get; set; }

        public Course ?Course { get; set; }


    }
}
