using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class EnrollManager
    {
        EnrollGateway aStudentGateway = new EnrollGateway();
        public List<Student> GetAllStudentRegNo()
        {
            return aStudentGateway.GetAllStudentRegNo();
        }

        public Student GetStudentDetailsWithRegNo(Student student)
        {
            return aStudentGateway.GetStudentDetailsWithRegNo(student);
        }

        public List<Course> GetAllCourses(int departmentId)
        {
            return aStudentGateway.GetAllCourses(departmentId);
        }

        public string Save(Student student)
        {
            if (aStudentGateway.IsCourseExists(student))
            {
                return "This course already enrolled";
            }
            else
            {
                int isRowAffected = aStudentGateway.Save(student);
                if (isRowAffected > 0)
                {
                    return "Course enrolled successfull";
                }
                else
                {
                    return "course enrolled failed";

                }
            }
        }

        public List<Course> GetAllEnrollCourses(int id)
        {

            return aStudentGateway.GetAllEnrollCourses(id);
        }

        public string SaveResult(Student student)
        {
            int isRowAffected = aStudentGateway.SaveResult(student);
            if (isRowAffected > 0)
            {
                return "Course result saved";
            }
            else
            {
                return "result adding failed ";

            }
        }
    }
}