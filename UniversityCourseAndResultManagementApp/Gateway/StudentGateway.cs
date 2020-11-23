using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class StudentGateway : Gateway
    {
        public List<Department> GetAllDepartments()
        {

            Query = "SELECT * FROM Department";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Department> Departments = new List<Department>();
            while (Reader.Read())
            {
                Department aDepartment = new Department();
                aDepartment.Id = (int) Reader["Id"];
                aDepartment.Code = Reader["Code"].ToString();

                Departments.Add(aDepartment);
            }
            Reader.Close();
            Connection.Close();
            return Departments;

        }

        public int Save(Student student)
        {
            Query =
                "INSERT INTO Student(Name,Email,ContactNo,Date,Address,DepartmentId) VALUES(@name, @email, @contactNo, @date, @address, @departmentId)";

            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = student.Name;

            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = student.Email;

            Command.Parameters.Add("contactNo", SqlDbType.VarChar);
            Command.Parameters["contactNo"].Value = student.ContactNo;

            Command.Parameters.Add("date", SqlDbType.Date);
            Command.Parameters["date"].Value = student.Date;

            Command.Parameters.Add("address", SqlDbType.VarChar);
            Command.Parameters["address"].Value = student.Address;

            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = student.DepartmentId;
            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public bool IsEmailExists(Student student)
        {
            Query = "SELECT Email FROM Student WHERE Email = @email";

            Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.Clear();
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = student.Email;
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = false;
            if (Reader.HasRows)
            {
                hasRow = true;
            }
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public string GetRegNo(string reg)
        {

            Query = "select count(RegNo) as Number from Student r where r.RegNo LIKE '%'+@regNo+'%'";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("regNo", SqlDbType.VarChar);
            Command.Parameters["regNo"].Value = reg;
            Connection.Open();
            Reader = Command.ExecuteReader();
            string data = "";
            if (Reader.Read())
            {
                int num = Convert.ToInt32(Reader["Number"]);
                num = num + 1;
                string nums = "00" + num;
                int a = nums.Length;
                string sub = nums.Substring(a - 3, 3);
                data = reg + "" + sub;

            }
            else
            {
                data = reg + "001";

            }
            Reader.Close();
            Connection.Close();
            return data;
        }

        public int Update(string data, Student student)
        {
            Query = "UPDATE Student SET RegNo = @data WHERE Email = @email";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("data", SqlDbType.VarChar);
            Command.Parameters["data"].Value = data;

            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = student.Email;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public string GetDepartment(Student student)
        {
            Query ="SELECT Code FROM Department WHERE Id = @departmentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = student.DepartmentId;
            Connection.Open();
            Reader = Command.ExecuteReader();
            string dept = "";
            Student aStudent = null;
            while (Reader.Read())
            {
                aStudent = new Student();
                aStudent.Department = Reader["Code"].ToString();
                dept = aStudent.Department;
            }
            Reader.Close();
            Connection.Close();
            return dept;
        }

        public Student GetStudent(Student student)
        {
            Query = "SELECT s.Name,s.Email,s.ContactNo,s.Address,d.Name as Department,s.RegNo FROM Student s JOIN Department d ON d.Id = s.DepartmentId WHERE Email = @email";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = student.Email;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student aStudent = null;
            while (Reader.Read())
            {
                aStudent = new Student();
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.ContactNo = Reader["ContactNo"].ToString();
                aStudent.Address = Reader["Address"].ToString();
                aStudent.Department = Reader["Department"].ToString();
                aStudent.RegNo = Reader["RegNo"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aStudent;
        }

        public List<Student> GetAllRegNos()
        {
            Query = "SELECT * FROM Student";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student();
                aStudent.Id = (int)Reader["Id"];
                aStudent.RegNo = Reader["RegNo"].ToString();

                students.Add(aStudent);
            }
            Reader.Close();
            Connection.Close();
            return students;
        }

        public List<StudentResultVM> GetAllCourseResult(int studentId)
        {
            Query =
                "Select  c.Code, c.Name,ce.Grade from CourseEnrollment ce inner join Course c on c.Id = ce.CourseId where ce.StudentId =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.VarChar);
            Command.Parameters["id"].Value = studentId;
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<StudentResultVM> studentResults = new List<StudentResultVM>();
            while (Reader.Read())
            {
                StudentResultVM studentResult = new StudentResultVM();
                studentResult.CourseCode = Reader["Code"].ToString();
                studentResult.CourseName = Reader["Name"].ToString();
                studentResult.Grade = Reader["Grade"].ToString();
                if (studentResult.Grade == "")
                {
                    studentResult.Grade = "Not Graded Yet";
                }
                studentResults.Add(studentResult);
            }
            Reader.Close();
            Connection.Close();
            return studentResults;
        }

        public Student GetStudentInfo(int studentId)
        {
            Query =
                "SELECT s.Name,s.Email,d.Name as DepartmentName FROM Student s JOIN Department d ON d.Id = s.DepartmentId WHERE s.Id = @studentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("studentId", SqlDbType.VarChar);
            Command.Parameters["studentId"].Value = studentId;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student aStudent = null;
            while (Reader.Read())
            {
                aStudent = new Student();
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.Department = Reader["DepartmentName"].ToString();
                
            }
            Reader.Close();
            Connection.Close();
            return aStudent;
        }

        public List<Course> GetAllEnrollCourses(int studentId)
        {
            Query = "Select c.Code, c.Name,ce.Id from Course c inner join CourseEnrollment ce on ce.CourseId = c.Id where ce.StudentId=@id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = studentId;
            Reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()
                };

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public int SaveResult(Student student)
        {
            Query = "INSERT Into StudentsResult Values(@studentId,@courseId,@grade)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("studentId", SqlDbType.Int);
            Command.Parameters["studentId"].Value = student.Id;
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = student.CourseId;
            Command.Parameters.Add("grade", SqlDbType.VarChar);
            Command.Parameters["grade"].Value = student.Grade;
            


            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public Student GetStudentInfoForPdf(Student student)
        {
            Query = "SELECT s.Name,s.Email,s.ContactNo,s.Address,d.Name as Department,s.RegNo FROM Student s JOIN Department d ON d.Id = s.DepartmentId WHERE s.Id = @studentId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("studentId", SqlDbType.Int);
            Command.Parameters["studentId"].Value = student.Id;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student aStudent = null;
            while (Reader.Read())
            {
                aStudent = new Student();
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.ContactNo = Reader["ContactNo"].ToString();
                aStudent.Address = Reader["Address"].ToString();
                aStudent.Department = Reader["Department"].ToString();
                aStudent.RegNo = Reader["RegNo"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aStudent;
        }
    }
}