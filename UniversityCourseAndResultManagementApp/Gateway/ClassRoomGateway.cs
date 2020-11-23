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
    public class ClassRoomGateway : Gateway
    {
        public List<Course> GetAllCourses(ClassRoom classRoom)
        {
            Query = "SELECT c.Id,c.Code, c.Name FROM Course c WHERE c.DepartmentId = @id";
            Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = classRoom.DepartmentId;

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int)Reader["Id"];
                course.Code = Reader["Code"].ToString();
                course.Name = Reader["Name"].ToString();
                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public List<ClassRoom> GetAllClassRooms(int id)
        {
            Query =
                "SELECT r.RoomNo,a.Day,a.FromTime,a.ToTime FROM AllocateClassroom a JOIN Room r ON r.Id = a.RoomId WHERE a.CourseId = @courseID";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = id;

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassRoom> classRooms = new List<ClassRoom>();
            while (Reader.Read())
            {
                ClassRoom aClassRoom = new ClassRoom();
                string sFrom = null;
                string sTo = null;
                int fHr;
                int tHr;
                int from = Convert.ToInt32(Reader["FromTime"]);
                if (from > 719)
                {
                    sFrom = "PM"; 
                }
                else
                {
                    sFrom = "AM";
                }
                int to = Convert.ToInt32(Reader["ToTime"]);
                if (to > 719)
                {
                    sTo = "PM";
                }
                else
                {
                    sTo = "AM";
                }
                string FromMin = Convert.ToString(from%60);
                if (FromMin.Length == 1)
                {
                    FromMin = FromMin + "0";
                }
                int FromHr = from/60;
                if (FromHr > 12)
                {
                    fHr = FromHr - 12;
                }
                else
                {
                    fHr = FromHr;
                }
                string ToMin = Convert.ToString(to % 60);
                if (ToMin.Length == 1)
                {
                    ToMin = ToMin + "0";
                }
                
                int ToHr = to / 60;
                if (ToHr > 12)
                {
                    tHr = ToHr - 12;
                }
                else
                {
                    tHr = ToHr;
                }
                

                aClassRoom.RoomNo = Reader["RoomNo"].ToString();
                aClassRoom.Day = Reader["Day"].ToString();
                aClassRoom.FromTime = fHr + ":" + FromMin+" "+sFrom; 
                aClassRoom.ToTime = tHr + ":" + ToMin+" "+sTo;
                
                
                classRooms.Add(aClassRoom);
            }
           
            Reader.Close();
            Connection.Close();
            return classRooms;
        }

        public bool IsRoomUnallocated()
        {
            Query = "SELECT Allocate FROM AllocateClassroom WHERE Allocate = @allocate";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("allocate", SqlDbType.Bit);
            Command.Parameters["allocate"].Value = 0;
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
        public int UnAllocateClassRoom()
        {
            Query = "Update AllocateClassroom SET Allocate = @allocate";
            Command = new SqlCommand(Query,Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("allocate", SqlDbType.Bit);
            Command.Parameters["allocate"].Value = 0;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }

/*      nahid          */

       /* public List<Course> GetAllCourses(ClassRoom classRoom)
        {
            Query = "SELECT c.Id,c.Code, c.Name FROM Course c WHERE c.DepartmentId = @id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = classRoom.DepartmentId;

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int)Reader["Id"];
                course.Code = Reader["Code"].ToString();
                course.Name = Reader["Name"].ToString();
                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public List<ClassRoom> GetAllClassRooms(int id)
        {
            Query =
                "SELECT r.RoomNo,a.Day,a.FromTime,a.ToTime FROM AllocateClassroom a JOIN Room r ON r.Id = a.RoomId WHERE a.CourseId = @courseID";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = id;

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassRoom> classRooms = new List<ClassRoom>();
            while (Reader.Read())
            {
                ClassRoom aClassRoom = new ClassRoom();
                int from = (int)Reader["FromTime"];
                int to = (int)Reader["ToTime"];

                int FromMin = from % 60;
                int FromHr = from / 60;
                int ToMin = to % 60;
                int ToHr = to / 60;


                aClassRoom.RoomNo = Reader["RoomNo"].ToString();
                aClassRoom.Day = Reader["Day"].ToString();
                aClassRoom.FromTime = FromHr + ":" + FromMin;
                aClassRoom.ToTime = ToHr + ":" + ToMin;


                classRooms.Add(aClassRoom);
            }

            Reader.Close();
            Connection.Close();
            return classRooms;
        }*/

    }
}