using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace 	P01_StudentSystem.Data.Models 
{
    public class Resource
    {
        public int ResourceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } 
        public string Url { get; set; } = null!;

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }
}
