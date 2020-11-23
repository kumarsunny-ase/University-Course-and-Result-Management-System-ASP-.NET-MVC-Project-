using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementApp.Gateway;
using UniversityCourseAndResultManagementApp.Models;

namespace UniversityCourseAndResultManagementApp.Manager
{
    public class TeacherManager
    {
        TeacherGateway anTeacherGateway = new TeacherGateway();
        public string SaveTeacher(Teacher teacher)
        {
            if (anTeacherGateway.IsEmailExist(teacher))
            {
                return "Email already exists";
            }
            else
            {
                int isRowAffected = anTeacherGateway.SaveTeacher(teacher);
                if (isRowAffected > 0)
                {
                    return "Teacher saved successfully";
                }
                else
                {
                    return "Teacher save failed";

                }
            };
        }

        public int AssaignCourseTeacher(Teacher teacher)
        {
            int assaigned = 0;
            
            if (anTeacherGateway.IsCourseAssaigned(teacher))
            {
                assaigned = 2;
            }
            else
            {
                int isRowAffected = anTeacherGateway.AssaignCourseTeacher(teacher);
                if (isRowAffected > 0)
                {
                    double remain = teacher.CreditLeft - teacher.Credit;
                    anTeacherGateway.UpdateTeacherCredit(remain, teacher.Id);
                    assaigned = 1;
                }
                else
                {
                    assaigned = 2;

                }
            }

            return assaigned;
        }

        public string GetAssaigned(int id)
        {
            return anTeacherGateway.GetAssaigned(id);
        }
    }
}