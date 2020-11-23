using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager anDepartmentManager = new DepartmentManager();
        CourseManager anCourseManager = new CourseManager();
        //
        // GET: /Course/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveCourse()
        {
            //ViewBag.departments = anDepartmentManager.GetAllDepartment();
            ViewBag.departments = GetDepartmentDropDownList();
            ViewBag.semesters = GetSemesterDropDownList();

            // ViewBag.semesters = anCourseManager.GetAllSemester();
            return View();
        }
        [HttpPost]
        public ActionResult SaveCourse(Course course)
        {

            //ViewBag.departments = anDepartmentManager.GetAllDepartment();
            ViewBag.departments = GetDepartmentDropDownList();
            //ViewBag.semesters = anCourseManager.GetAllSemester();
            ViewBag.semesters = GetSemesterDropDownList();
            ViewBag.msg = anCourseManager.Save(course);
            return View();
        }
        private List<SelectListItem> GetDepartmentDropDownList()
        {
            var departments = anDepartmentManager.GetAllDepartment();
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Value="", Text="---Select---"}
            };
            foreach (Department dept in departments)
            {
                SelectListItem item = new SelectListItem() { Value = dept.Id.ToString(), Text = dept.Name };
                items.Add(item);
            }
            return items;
        }

        private List<SelectListItem> GetSemesterDropDownList()
        {
            var semesters = anCourseManager.GetAllSemester();
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Value="", Text="---Select---"}
            };
            foreach (Semester semester in semesters)
            {
                SelectListItem item = new SelectListItem() { Value = semester.Id.ToString(), Text = semester.Name };
                items.Add(item);
            }
            return items;
        }

    }
}