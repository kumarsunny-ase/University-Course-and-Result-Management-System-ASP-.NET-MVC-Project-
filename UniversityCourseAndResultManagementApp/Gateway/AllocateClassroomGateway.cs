using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class AllocateClassroomGateway:Gateway
    {
        public List<Department> GetAllDepartments()
        {
            Query = "SELECT * FROM Department";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Department> departments = new List<Department>();

            while (Reader.Read())
            {
                Department department = new Department()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["Name"].ToString(),
                    Code = Reader["Code"].ToString()
                };
                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();
            return departments;
        }


        public List<Room> GetAllRooms()
        {

            Query = "SELECT * FROM Room";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();

            Reader = Command.ExecuteReader();
            List<Room> rooms = new List<Room>();
            while (Reader.Read())
            {
                Room room = new Room()
                {
                    Id = (int)Reader["Id"],
                    Name = Reader["RoomNo"].ToString()
                };
                rooms.Add(room);

            }
            Reader.Close();
            Connection.Close();
            return rooms;
        }

        public List<Course> GetAllCourses(AllocateClassroom allocateClassroom)
        {
            Query = "SELECT * FROM Course Where DepartmentId = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = allocateClassroom.DepartmentId;
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
        public List<AllocateClassroom> IsScheduleExists(AllocateClassroom allocateClassroom)
        {

            Query = "SELECT * FROM AllocateClassroom WHERE RoomId = @roomId AND Day = @day AND Allocate =@allocate ";
            Command = new SqlCommand(Query, Connection);

            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = allocateClassroom.RoomId;

            Command.Parameters.Add("day", SqlDbType.VarChar);
            Command.Parameters["day"].Value = allocateClassroom.Day;

            Command.Parameters.Add("allocate", SqlDbType.Bit);
            Command.Parameters["allocate"].Value = 1;
            Reader = Command.ExecuteReader();
            List<AllocateClassroom> allocateClassrooms = new List<AllocateClassroom>();

            if (Reader.Read())
            {
                AllocateClassroom classroom = new AllocateClassroom()
                {
                    FromTime = Reader["FromTime"].ToString(),
                    ToTime = Reader["ToTime"].ToString(),
                    Allocate = Reader["Allocate"].ToString()


                };
                allocateClassrooms.Add(classroom);
            }
            Reader.Close();
            Connection.Close();
            return allocateClassrooms;
        }

        public int Save(AllocateClassroom allocateClassroom)
        {
            Query = "INSERT INTO AllocateClassroom VALUES (@deptId, @courseId, @roomId, @from, @to, @allocate, @day)";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            string fromH = allocateClassroom.FromTime;
            string fromHSub = fromH.Substring(0, 2);
            string fromM = allocateClassroom.FromTime;
            string fromMSub = fromM.Substring(3, 2);
            int fromMin = Convert.ToInt32(fromHSub) * 60 + Convert.ToInt32(fromMSub);
            string toH = allocateClassroom.ToTime;
            string toHSub = toH.Substring(0, 2);
            string toM = allocateClassroom.ToTime;
            string toMSub = toM.Substring(3, 2);
            int toMin = Convert.ToInt32(toHSub) * 60 + Convert.ToInt32(toMSub);
            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = allocateClassroom.DepartmentId;
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = allocateClassroom.CourseId;

            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = allocateClassroom.RoomId;

            Command.Parameters.Add("from", SqlDbType.Int);
            Command.Parameters["from"].Value = fromMin;

            Command.Parameters.Add("to", SqlDbType.Int);
            Command.Parameters["to"].Value = toMin;

            Command.Parameters.Add("allocate", SqlDbType.Bit);
            Command.Parameters["allocate"].Value = 1;

            Command.Parameters.Add("day", SqlDbType.VarChar);
            Command.Parameters["day"].Value = allocateClassroom.Day;

            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }
    }
}