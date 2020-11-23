using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementApp.Manager;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager anDepartmentManager = new DepartmentManager();
        TeacherManager anTeacherManager = new TeacherManager();
        CourseManager anCourseManager = new CourseManager();
        //
        // GET: /Department/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            ViewBag.msg = anDepartmentManager.SaveDepartment(department);
            return View();
        }

        public ActionResult ViewDepartment()
        {
            ViewBag.departments = anDepartmentManager.GetAllDepartment();
            return View();
        }


        public ActionResult SaveTeacher()
        {
            ViewBag.departments = GetDepartmentDropDownList();
            ViewBag.designations = GetDesignationDropDownList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher teacher)
        {
            ViewBag.departments = GetDepartmentDropDownList();
            ViewBag.designations = GetDesignationDropDownList();
            ViewBag.msg = anTeacherManager.SaveTeacher(teacher);
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

        private List<SelectListItem> GetDesignationDropDownList()
        {
            var designations = anDepartmentManager.GetAllDesignation();
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Value="", Text="---Select---"}
            };
            foreach (Teacher teacher in designations)
            {
                SelectListItem item = new SelectListItem() { Value = teacher.DesignationId.ToString(), Text = teacher.DesignationName };
                items.Add(item);
            }
            return items;
        }


        public ActionResult CourseAssaignTeacher()
        {
            ViewBag.departments = anDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssaignTeacher(Teacher teacher)
        {
            ViewBag.departments = anDepartmentManager.GetAllDepartment();
            ViewBag.assaigned = anTeacherManager.AssaignCourseTeacher(teacher);
            return View();
        }

        public JsonResult GetTeacherCourseByDepartmentId(int id)
        {
            var teacherInfo = anDepartmentManager.GetTeacherByDepartmentId(id);
            var courseInfo = anDepartmentManager.GetCourseByDepartmentId(id);
            Department department = new Department();
            department.Courses = courseInfo;
            department.Teachers = teacherInfo;
            return Json(department);
        }

        public JsonResult GetTeacherInfoById(int id)
        {
            var teacherInfo = anDepartmentManager.GetTeacherInfoById(id);
            return Json(teacherInfo);
        }
        public JsonResult GetCourseInfoById(int id)
        {
            var courseInfo = anDepartmentManager.GetCourseInfoById(id);
            return Json(courseInfo);
        }

        public ActionResult ViewCourseStatics()
        {
            ViewBag.departments = anDepartmentManager.GetAllDepartment();

            return View();
        }
        [HttpPost]
        public ActionResult ViewCourseStatics(int id)
        {
            ViewBag.departments = anDepartmentManager.GetAllDepartment();

            return View();
        }
        public JsonResult GetCoursesByDepartmentId(int id)
        {
            var courses = anCourseManager.GetAllCourses(id);

            foreach (Course course in courses)
            {

                course.TeacherName = anTeacherManager.GetAssaigned(course.Id).ToString();
            }


            return Json(courses);
        }

        public ActionResult UnassignCourses()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourses(Course course)
        {
            ViewBag.Assigned = 
            ViewBag.Message = anDepartmentManager.UnassignCourses();

            return View();
        }
        public JsonResult IsCourseAssigned(int aVal)
         {


             var returnObj = anDepartmentManager.IsCourseAssigned(aVal);
        

            return Json(returnObj);
          }
    }
}