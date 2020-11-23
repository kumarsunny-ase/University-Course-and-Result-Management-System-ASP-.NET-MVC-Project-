using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway anDepartmentGateway = new DepartmentGateway();
        public string SaveDepartment(Department department)
        {
            if (anDepartmentGateway.IsDepartmentExist(department))
            {
                return "Department already exists";
            }
            else
            {
                int isRowAffected = anDepartmentGateway.SaveDepartment(department);
                if (isRowAffected > 0)
                {
                    return "Department saved successfully";
                }
                else
                {
                    return "Department save failed";

                }
            }
        }

        public List<Department> GetAllDepartment()
        {
            return anDepartmentGateway.GetAllDepartment();
        }

        public IEnumerable<Teacher> GetAllDesignation()
        {
            return anDepartmentGateway.GetAllDesignation();
        }


        public List<Teacher> GetTeacherByDepartmentId(int id)
        {
            return anDepartmentGateway.GetTeacherByDepartmentId(id);
        }

        public List<Course> GetCourseByDepartmentId(int id)
        {
            return anDepartmentGateway.GetCourseByDepartmentId(id);

        }

        public Teacher GetTeacherInfoById(int id)
        {
            return anDepartmentGateway.GetTeacherInfoById(id);
        }

        public Course GetCourseInfoById(int id)
        {
            return anDepartmentGateway.GetCourseInfoById(id);
        }

        public string UnassignCourses()
        {
            if (anDepartmentGateway.IsCoursesAssigned())
            {
                return "Course already unassigned";

            }
            else
            {
                int rowAffected = anDepartmentGateway.UnassignCourses();
                if (rowAffected > 0)
                {
                    return "Course unassigned";
                }
                else
                {
                    return "Course unassigne falied";
                }
            }
        }

        public int IsCourseAssigned(int aVal)
        {

            if (anDepartmentGateway.IsCourseAssigned(aVal))
            {
                return 1;
            }
            return 0;
        }
    }
}