using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementApp.Models
{
    public class AllocateClassroom
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Allocate { get; set; }
        public string Day { get; set; }

    }
}