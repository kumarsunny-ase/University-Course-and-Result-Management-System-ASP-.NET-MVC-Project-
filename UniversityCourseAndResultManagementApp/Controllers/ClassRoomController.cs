using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class ClassRoomController : Controller
    {
        ClassRoomManager aClassRoomManager = new ClassRoomManager();
        StudentManager aStudentManager = new StudentManager();
        public ActionResult ShowClassSchedule()
        {
           
            ViewBag.Departments = aStudentManager.GetAllDepartments();
           // ViewBag.Departments = GetDepartmentForDropDownList();

            //ViewBag.ClassSchedule = aClassRoomManager.GetAllAllocatedClassRoomWithout();
            return View();
        }
        [HttpPost]
        public ActionResult ShowClassSchedule(ClassRoom classRoom)
        {
            ViewBag.Departments = aStudentManager.GetAllDepartments();

            //ViewBag.Departments = GetDepartmentForDropDownList();
            /*List<Course> courses = aClassRoomManager.GetAllCourses(classRoom);
            ViewBag.Courses = courses;
            foreach (Course course in courses)
            {
                List<ClassRoom> classRooms = aClassRoomManager.GetAllClassRooms(course);
                ViewBag.ClassRooms = classRooms;
                return View();
            }*/
            
            return View();
        }
        public JsonResult GetCoursesByDepartmentId(ClassRoom classRoom)
        {
            var courses = aClassRoomManager.GetAllCourses(classRoom);


            List<Course> courseDetails = new List<Course>();
            foreach (Course course in courses)
            {
                List<ClassRoom> classRooms = aClassRoomManager.GetAllClassRooms(course.Id);
                if (classRooms.Count<1)
                {
                    course.RoomInfo = "Not scheduled yet";
                    
                }
                else
                {
                    foreach (ClassRoom room in classRooms)
                    {
                        course.RoomInfo += "R No: " + room.RoomNo + ", " + room.Day + ", " + room.FromTime + "-" +
                                           room.ToTime + "; </br> ";
                    } 
                }
                course.ClassRooms = classRooms;
                course.Code = course.Code;
                course.Name = course.Name;
                courseDetails.Add(course);
            }

            return Json(courseDetails);
        }

        public ActionResult AllocateClassRoom()
        {
            return View();
        }
        public ActionResult UnAllocateClassRoom()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult UnAllocateClassRoom(ClassRoom classRoom)
        {
            ViewBag.Message = aClassRoomManager.UnAllocateClassRoom();
            return View();
        }
       
	}
}