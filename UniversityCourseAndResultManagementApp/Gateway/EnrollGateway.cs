using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class EnrollGateway:Gateway
    {
        public List<Student> GetAllStudentRegNo()
        {

            Query = "SELECT * FROM STUDENT";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();

            while (Reader.Read())
            {

                Student student = new Student()
                {
                    Id = (int)Reader["Id"],
                    RegNo = Reader["RegNo"].ToString()
                };
                students.Add(student);
            }
            Reader.Close();
            Connection.Close();
            return students;
        }

        public Student GetStudentDetailsWithRegNo(Student student)
        {
            Query = "select s.Name, s.Email, s.DepartmentId,d.Code from Student s inner join Department d on d.Id = s.DepartmentId where s.Id = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = student.Id;
            Reader = Command.ExecuteReader();
            Student studentInfo = new Student();
            while (Reader.Read())
            {

                studentInfo.Name = Reader["Name"].ToString();
                studentInfo.Email = Reader["Email"].ToString();
                studentInfo.Code = Reader["Code"].ToString();
                studentInfo.DepartmentId = Convert.ToInt32(Reader["DepartmentId"].ToString());
            }
            Reader.Close();
            Connection.Close();
            return studentInfo;
        }

        public List<Course> GetAllCourses(int departmentId)
        {
            Query = "SELECT * FROM Course Where DepartmentId = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = departmentId;
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

        public bool IsCourseExists(Student student)
        {
            Query = "SELECT * FROM CourseEnrollment Where StudentId = @sId AND CourseId = @cId ";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("sId", SqlDbType.Int);
            Command.Parameters["sId"].Value = student.Id;
            Command.Parameters.Add("cId", SqlDbType.Int);
            Command.Parameters["cId"].Value = student.CourseId;
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

        public int Save(Student student)
        {
            Query = "INSERT INTO CourseEnrollment VALUES(@sId, @cId, @date, @grade)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();

            Command.Parameters.Add("sId", SqlDbType.Int);
            Command.Parameters["sId"].Value = student.Id;
            Command.Parameters.Add("cId", SqlDbType.Int);
            Command.Parameters["cId"].Value = student.CourseId;

            Command.Parameters.Add("date", SqlDbType.Date);
            Command.Parameters["date"].Value = student.Date;

            Command.Parameters.Add("grade", SqlDbType.VarChar);
            Command.Parameters["grade"].Value = "";
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public List<Course> GetAllEnrollCourses(int id)
        {
            Query = "Select c.Code, c.Name,ce.Id from Course c inner join CourseEnrollment ce on ce.CourseId = c.Id where ce.StudentId=@id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
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
            Query = "UPDATE CourseEnrollment SET Grade=@grade WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();

            Command.Parameters.Add("grade", SqlDbType.VarChar);
            Command.Parameters["grade"].Value = student.Grade;
            Command.Parameters.Add("Id", SqlDbType.Int);
            Command.Parameters["Id"].Value = student.CourseId;


            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }
    }
}