using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementApp.Models
{
    public class Student
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter student name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter student email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter valid email")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter student contact no.")]
        [StringLength(14,MinimumLength = 11,ErrorMessage = "Contact no must be between 11 to 14 digit")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contact no must be numeric")]
        [DisplayName("Contact No")]

        public string ContactNo { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter student address")]
        public string Address { get; set; }
        
        public string Department { get; set; }
        [Required(ErrorMessage = "Please select a department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Reg No")]

        public string RegNo { get; set; }
        public string Year { get; set; }
        public string Grade { get; set; }
        public int GradeId { get; set; }
        public string Course { get; set; }
        public int CourseId { get; set; }
        public List<Course> Courses { get; set; }

        public string Code { get; set; }
       
  
    }
}