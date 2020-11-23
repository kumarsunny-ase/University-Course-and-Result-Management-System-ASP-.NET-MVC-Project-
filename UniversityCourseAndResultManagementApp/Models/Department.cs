using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementApp.Models
{
    public class Department
    {
       public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Department Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Length must be 2 to 7 characters")]

        public String Code { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        public String Name { get; set; }

        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }

    }
}