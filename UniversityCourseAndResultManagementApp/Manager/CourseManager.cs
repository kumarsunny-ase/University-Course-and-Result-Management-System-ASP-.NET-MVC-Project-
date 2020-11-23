using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class CourseManager
    {
        CourseGateway anCourseGateway = new CourseGateway();
        public List<Semester> GetAllSemester()
        {
            return anCourseGateway.GetAllSemester();
        }

        public string Save(Course course)
        {
            if (anCourseGateway.IsCourseExist(course))
            {
                return "Course already exists";
            }
            else
            {
                int isRowAffected = anCourseGateway.SaveCourse(course);
                if (isRowAffected > 0)
                {
                    return "Course saved successfully";
                }
                else
                {
                    return "Course save failed";

                }
            }
        }

        public IEnumerable<Course> GetAllCourses(int id)
        {
            return anCourseGateway.GetAllCourses(id);
        }
    }
}