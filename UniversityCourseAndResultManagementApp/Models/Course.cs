using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementApp.Models
{
    public class Course
    {
        public string Department { get; set; }
        public string Semester { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Course Name")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Please Enter Course Code")]

        [StringLength(20, MinimumLength = 5, ErrorMessage = "code length Minimum 5 characters")]

        public String Code { get; set; }
        [Required(ErrorMessage = "Please Enter Course Credit")]
        [Range(0.5, 5.0, ErrorMessage = "Invaild range! Credit range is 0.5 to 5.0")]
        public double Credit { get; set; }
        [Required(ErrorMessage = "Please Enter Course Description")]

        public String Description { get; set; }
        [Required(ErrorMessage = "Please select department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select semester")]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        public string SemName { get; set; }
        public string RoomInfo { get; set; }
        public List<ClassRoom> ClassRooms { get; set; }
        public List<Department> Departments { get; set; }
        public string TeacherName { get; set; }
        public List<Semester> Semesters { get; set; }
    }
}