using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Rotativa;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class StudentController : Controller
    {
        StudentManager aStudentManager = new StudentManager();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Save()
        {

            ViewBag.Departments = GetDepartmentForDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Student student)
        {
            ViewBag.Departments = GetDepartmentForDropDownList();

            DateTime myDateTime = new DateTime();
            myDateTime = DateTime.ParseExact(student.Date, "dd-MM-yyyy", null);
            String date = myDateTime.ToString("yyyy-MM-dd");
            student.Date = date;
            //student.Department = aStudentManager.GetDepartment(student);

            String year = myDateTime.Year.ToString();           
            //student.RegNo = student.Department + "-" + year + "-";
            student.RegNo = year;

            
            ViewBag.Message = aStudentManager.Save(student);
            
            string email = student.Email;
            TempData["email"] = email;
            if (ViewBag.Message == "1")
            {
                //ShowStudentInfo(student);
                return RedirectToAction("ShowStudentInfo","Student");
            }

            return View();

        }
        private List<SelectListItem> GetDepartmentForDropDownList()
        {
            var departments = aStudentManager.GetAllDepartments();

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Value = "", Text = "--Select--"}
            };

            foreach (Department dept in departments)
            {
                SelectListItem item = new SelectListItem() { Value = dept.Id.ToString(), Text = dept.Code };
                items.Add(item);
            }

            return items;
        }
        [HttpGet]
        public ActionResult ShowStudentInfo(Student aStudent)
        {
            

            Student student = new Student();
            student.Email = TempData["email"].ToString();
            //string email = aStudent.Email;

           Student student1 = aStudentManager.GetStudent(student);
           ViewBag.Student = student1;
            return View();
        }

        public ActionResult ViewStudentResult()
        {
            ViewBag.RegNos = aStudentManager.GetAllRegNos();
            return View();
        }
        [HttpPost]
        public ActionResult ViewStudentResult(Student student)
        {
            ViewBag.RegNos = aStudentManager.GetAllRegNos();
            
            student.Id = (int)TempData["Id"];
            
            
            return RedirectToAction("GeneratePdf", "Student");
            return View();
        }
        public JsonResult GetStudentInfoById(int studentId)
        {
            Student aStudent = aStudentManager.GetStudentInfo(studentId);
            TempData["Id"] = studentId;
            
            return Json(aStudent);
        }
        public JsonResult GetStudentResultByStudentId(int studentId)
        {
            
            List<StudentResultVM> studentResults = aStudentManager.GetAllCourseResult(studentId);

            return Json(studentResults);
        }

        //public ActionResult ShowResult(Student student)
        //{
            
        //    return View();
        //}



        public ActionResult GeneratePdf(Student student)
        {

            student.Id = (int)TempData["Id"];
            ViewBag.Student = aStudentManager.GetStudentInfoForPdf(student);

            List<StudentResultVM> studentResults = aStudentManager.GetAllCourseResult(student.Id);
            ViewBag.Result = studentResults;
            return new ViewAsPdf("GeneratePdf", "Student")
            {
                FileName = "Result sheet.pdf"
            };
        }
        
	}
}