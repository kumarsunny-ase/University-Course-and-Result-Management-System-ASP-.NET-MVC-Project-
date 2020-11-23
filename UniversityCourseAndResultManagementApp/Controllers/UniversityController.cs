using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class UniversityController : Controller
    {
        AllocateClassroomManager ClassroomManager = new AllocateClassroomManager();
        //
        // GET: /University/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AllocateClassroom()
        {
            ViewBag.departments = ClassroomManager.GetAllDepartments();
            ViewBag.rooms = ClassroomManager.GetAllRooms();

            return View();
        }

        [HttpPost]

        public ActionResult AllocateClassroom(AllocateClassroom allocateClassroom)
        {
            ViewBag.departments = ClassroomManager.GetAllDepartments();
            ViewBag.rooms = ClassroomManager.GetAllRooms();
            ViewBag.msg = ClassroomManager.Save(allocateClassroom);
            return View();
        }
        public JsonResult GetCourseByDepartmentId(AllocateClassroom allocateClassroom)
        {
            var courses = ClassroomManager.GetAllCourses(allocateClassroom);
            return Json(courses);
        }
    }
}