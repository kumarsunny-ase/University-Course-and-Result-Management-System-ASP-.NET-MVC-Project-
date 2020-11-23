using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class StudentManager
    {
        private StudentGateway aStudentGateway = new StudentGateway();

        public List<Department> GetAllDepartments()
        {
            return aStudentGateway.GetAllDepartments();
        }

        public string Save(Student student)
        {
            if (aStudentGateway.IsEmailExists(student))
            {
                return "Enail already exists...";
            }
            else
            {
                int rowAffected = aStudentGateway.Save(student);
                if (rowAffected > 0)
                {
                    string deptcode = aStudentGateway.GetDepartment(student);
                    string reg = deptcode + "-" + student.RegNo+"-";
                    student.RegNo = aStudentGateway.GetRegNo(reg);
                    int rowAffected2 = aStudentGateway.Update(student.RegNo, student);
                    if (rowAffected2 > 0)
                    {
                        return "1";
                    }
                    else
                    {
                        return "Register falied";
                    }

                }
                else
                {
                    return "Register failed";
                }
            }
        }

        public string GetDepartment(Student student)
        {
            return aStudentGateway.GetDepartment(student);
        }

        public Student GetStudent(Student student)
        {
            return aStudentGateway.GetStudent(student);
        }

        public List<Student> GetAllRegNos()
        {
            return aStudentGateway.GetAllRegNos();
        }

        public List<StudentResultVM> GetAllCourseResult(int studentId)
        {
            return aStudentGateway.GetAllCourseResult(studentId);
        }

        public Student GetStudentInfo(int studentId)
        {
            return aStudentGateway.GetStudentInfo(studentId);
        }

        public List<Course> GetAllEnrollCourses(int studentId)
        {
            return aStudentGateway.GetAllEnrollCourses(studentId);
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

/*                 */
       /* public List<Student> GetAllStudentRegNo()
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
        }*/

        public Student GetStudentInfoForPdf(Student student)
        {
            return aStudentGateway.GetStudentInfoForPdf(student);
        }
    }
}