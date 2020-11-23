using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class CourseGateway : Gateway
    {
        public List<Semester> GetAllSemester()
        {
            Query = "SELECT * FROM SEMESTER";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Semester> semesters = new List<Semester>();
            while (Reader.Read())
            {
                Semester semester = new Semester()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()
                };
                semesters.Add(semester);
            }
            Reader.Close();
            Connection.Close();
            return semesters;
        }

        public bool IsCourseExist(Course course)
        {
            Query = "SELECT * FROM Course Where Code = @code or Name = @name ";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = course.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = course.Name;
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

        public int SaveCourse(Course course)
        {
            Query = "INSERT INTO Course VALUES(@code, @name,@credit,@description,@dId,@sId)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = course.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = course.Name;
            Command.Parameters.Add("credit", SqlDbType.Decimal);
            Command.Parameters["credit"].Value = course.Credit;
            Command.Parameters.Add("description", SqlDbType.VarChar);
            Command.Parameters["description"].Value = course.Description;
            Command.Parameters.Add("dId", SqlDbType.Int);
            Command.Parameters["dId"].Value = course.DepartmentId;
            Command.Parameters.Add("sId", SqlDbType.Int);
            Command.Parameters["sId"].Value = course.SemesterId;
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public IEnumerable<Course> GetAllCourses(int id)
        {
            Query = "select c.id, c.Code, c.Name, s.Name as SemName from Course c inner join Semester s on s.Id = c.SemesterId inner join Department d on d.Id = c.DepartmentId where d.Id =@id";
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
                    Name = Reader["Name"].ToString(),
                    Code = Reader["Code"].ToString(),
                    SemName = Reader["SemName"].ToString()
                };
                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }
    }
}