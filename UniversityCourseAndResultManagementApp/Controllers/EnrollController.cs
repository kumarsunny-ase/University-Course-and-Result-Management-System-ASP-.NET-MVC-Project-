using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class EnrollController : Controller
    {
        EnrollManager anStudentManger = new EnrollManager();
        //
        // GET: /Enroll/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save()
        {
            ViewBag.students = anStudentManger.GetAllStudentRegNo();
            return View();
        }
        [HttpPost]
        public ActionResult Save(Student student)
        {
            ViewBag.students = anStudentManger.GetAllStudentRegNo();
            ViewBag.msg = anStudentManger.Save(student);
            return View();
        }
        public JsonResult GetStudentInfo(Student student)
        {
            var studentInfo = anStudentManger.GetStudentDetailsWithRegNo(student);
            var courses = anStudentManger.GetAllCourses(studentInfo.DepartmentId);
            studentInfo.Courses = courses;
            return Json(studentInfo);
        }

        public ActionResult SaveResult()
        {
            ViewBag.students = anStudentManger.GetAllStudentRegNo();

            return View();
        }
        [HttpPost]
        public ActionResult SaveResult(Student student)
        {

            ViewBag.students = anStudentManger.GetAllStudentRegNo();
            ViewBag.msg = anStudentManger.SaveResult(student);

            return View();
        }

        public JsonResult GetStudentInformation(Student student)
        {
            var studentInfo = anStudentManger.GetStudentDetailsWithRegNo(student);
            var courses = anStudentManger.GetAllEnrollCourses(student.Id);
            studentInfo.Courses = courses;
            return Json(studentInfo);
        }
    }
}