using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Gateway
{
    public class TeacherGateway : Gateway
    {
        public bool IsEmailExist(Teacher teacher)
        {
            Query = "SELECT * FROM Teacher Where Email = @email";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = teacher.Email;
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

        public int SaveTeacher(Teacher teacher)
        {
            Query = "INSERT INTO Teacher VALUES(@name, @email,@address,@contactNo,@CreditToBeTaken,@RemainingCredit,@DesignationId,@DepartmentId)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = teacher.Name;

            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = teacher.Email;

            Command.Parameters.Add("address", SqlDbType.VarChar);
            Command.Parameters["address"].Value = teacher.Address;

            Command.Parameters.Add("contactNo", SqlDbType.VarChar);
            Command.Parameters["contactNo"].Value = teacher.ContactNo;

            Command.Parameters.Add("CreditToBeTaken", SqlDbType.Decimal);
            Command.Parameters["CreditToBeTaken"].Value = teacher.CreditTake;

            Command.Parameters.Add("RemainingCredit", SqlDbType.Decimal);
            Command.Parameters["RemainingCredit"].Value = teacher.CreditTake;

            Command.Parameters.Add("DesignationId", SqlDbType.Int);
            Command.Parameters["DesignationId"].Value = teacher.DesignationId;

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = teacher.DepartmentId;
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public bool IsCourseAssaigned(Teacher teacher)
        {
            Query = "SELECT * FROM CourseAssignToTeacher Where CourseId = @cId AND Assign = @assign";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("cId", SqlDbType.Int);
            Command.Parameters["cId"].Value = teacher.CourseId;
            Command.Parameters.Add("assign", SqlDbType.Bit);
            Command.Parameters["assign"].Value = 1;
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

        public int AssaignCourseTeacher(Teacher teacher)
        {
            Query = "INSERT INTO CourseAssignToTeacher VALUES(@deptId, @courseId,@teacherId,@assign)";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = teacher.CourseId;

            Command.Parameters.Add("teacherId", SqlDbType.Int);
            Command.Parameters["teacherId"].Value = teacher.Id;
            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = teacher.DepartmentId;

            Command.Parameters.Add("assign", SqlDbType.Int);
            Command.Parameters["assign"].Value = 1;
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
            return isRowEffected;
        }

        public void UpdateTeacherCredit(double remain, int id)
        {
            Query = "UPDATE Teacher SET RemainingCredit =@remain WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Command.Parameters.Clear();

            Command.Parameters.Add("remain", SqlDbType.Decimal);
            Command.Parameters["remain"].Value = remain;

            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            int isRowEffected = Command.ExecuteNonQuery();
            Connection.Close();
        }

        public string GetAssaigned(int id)
        {
            Query = "select t.Name as teacherName from Course c inner join CourseAssignToTeacher ca on c.Id = ca.CourseId inner join Teacher t on t.Id = ca.TeacherId where c.Id =@id";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();

            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            Reader = Command.ExecuteReader();
            string assaigned = "Not Assigned Yet";
            while (Reader.Read())
            {

                assaigned = Reader["teacherName"].ToString();

            }
            Reader.Close();
            Connection.Close();
            return assaigned;
        }
    }
}