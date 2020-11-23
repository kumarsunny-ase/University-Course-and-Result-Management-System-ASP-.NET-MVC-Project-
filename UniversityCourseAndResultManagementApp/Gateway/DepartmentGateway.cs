using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class DepartmentGateway : Gateway
    {
        public bool IsDepartmentExist(Department department)
        {
            Query = "SELECT * FROM Department Where Code = @code or Name = @name ";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = department.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = department.Name;
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

        public int SaveDepartment(Department department)
        {
            Query = "INSERT INTO Department VALUES(@code, @name)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = department.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = department.Name;
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public List<Department> GetAllDepartment()
        {
            Query = "SELECT * FROM DEPARTMENT";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Department> departments = new List<Department>();

            while (Reader.Read())
            {

                Department department = new Department()
                {
                    Id = (int)Reader["Id"],
                    Code = Reader["Code"].ToString(),
                    Name = Reader["Name"].ToString()

                };
                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();
            return departments;
        }

        public IEnumerable<Teacher> GetAllDesignation()
        {

            Query = "SELECT * FROM Designation";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Teacher> designations = new List<Teacher>();

            while (Reader.Read())
            {

                Teacher teacher = new Teacher()
                {

                    DesignationId = Convert.ToInt32(Reader["Id"].ToString()),
                    DesignationName = Reader["Designation"].ToString()

                };
                designations.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return designations;
        }


        public List<Teacher> GetTeacherByDepartmentId(int id)
        {
            Query = "SELECT * FROM Teacher Where DepartmentId = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            Reader = Command.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString()
                };

                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public List<Course> GetCourseByDepartmentId(int id)
        {
            Query = "SELECT * FROM Course Where DepartmentId = @id";
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
                    Code = Reader["Code"].ToString()
                };

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public Teacher GetTeacherInfoById(int id)
        {
            Query = "SELECT * FROM Teacher Where Id = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            Reader = Command.ExecuteReader();
            Teacher teacher = null;

            while (Reader.Read())
            {
                teacher = new Teacher()
                {
                    CreditTake = Convert.ToDouble(Reader["CreditToBeTaken"].ToString()),
                    CreditLeft = Convert.ToDouble(Reader["RemainingCredit"].ToString())
                };
            }
            Reader.Close();
            Connection.Close();
            return teacher;
        }

        public Course GetCourseInfoById(int id)
        {
            Query = "SELECT * FROM Course Where Id = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            Reader = Command.ExecuteReader();
            Course course = null;

            while (Reader.Read())
            {
                course = new Course()
                {
                    Name = Reader["Name"].ToString(),
                    Credit = Convert.ToDouble(Reader["Credit"].ToString())
                };
            }
            Reader.Close();
            Connection.Close();
            return course;
        }

        public bool IsCoursesAssigned()
        {
            Query = "SELECT Assign FROM CourseAssignToTeacher WHERE Assign = @assign";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("assign", SqlDbType.Bit);
            Command.Parameters["assign"].Value = 0;
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = false;
            while (Reader.Read())
            {
                hasRow = true;
            }
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int UnassignCourses()
        {
            Query = "Update CourseAssignToTeacher SET Assign = @assign";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("assign", SqlDbType.Bit);
            Command.Parameters["assign"].Value = 0;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public bool IsCourseAssigned(int aVal)
        {
            Query = "SELECT Assign FROM CourseAssignToTeacher WHERE Assign = @assign";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("assign", SqlDbType.Bit);
            Command.Parameters["assign"].Value = aVal;
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = false;
            while (Reader.Read())
            {
                hasRow = true;
            }
            Reader.Close();
            Connection.Close();
            return hasRow;
        }
    }
}