using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class AllocateClassroomManager
    {
        AllocateClassroomGateway ClassroomGateway = new AllocateClassroomGateway();
        public List<Department> GetAllDepartments()
        {
            return ClassroomGateway.GetAllDepartments();
        }


        public List<Room> GetAllRooms()
        {
            return ClassroomGateway.GetAllRooms();
        }

        public List<Course> GetAllCourses(AllocateClassroom allocateClassroom)
        {
            return ClassroomGateway.GetAllCourses(allocateClassroom);
        }
        /*public List<Course> GetAllCourses()
        {
            return ClassroomGateway.GetAllCourses();
        }*/

        public string Save(AllocateClassroom allocateClassroom)
        {
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

            List<AllocateClassroom> classrooms = ClassroomGateway.IsScheduleExists(allocateClassroom);
            if (classrooms.Count > 0)
            {
                foreach (AllocateClassroom allocate in classrooms)
                {

                    int from = Convert.ToInt32(allocate.FromTime);
                    int to = Convert.ToInt32(allocate.ToTime);
                    string allocation = allocate.Allocate;
                    if (fromMin >= from && fromMin < to)
                    {
                        return "Schedule allready allocated";
                    }
                    else if (toMin > from && toMin <= to)
                    {
                        return "Schedule allready allocated";
                    }
                    else if (fromMin <= from && toMin >= to)
                    {

                        return "Schedule allready allocated";
                    }
                    else
                    {
                        //Save(allocateClassroom);
                        int isRowAffected = ClassroomGateway.Save(allocateClassroom);
                        if (isRowAffected > 0)
                        {
                            return "Room Allocated successfull";
                        }
                        else
                        {
                            return "Room Allocate failed";

                        }
                    }

                }
                return "Room Allocated successfull";

            }
            else
            {
                int isRowAffected = ClassroomGateway.Save(allocateClassroom);
                if (isRowAffected > 0)
                {
                    return "Room Allocated successfull";
                }
                else
                {
                    return "Room Allocate failed";

                }
            }
        }
    }
}