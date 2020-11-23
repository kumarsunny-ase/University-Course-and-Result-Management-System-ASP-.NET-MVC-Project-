using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher Name")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter an valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher contact no")]
        [DisplayName("Contact No")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "Contact no must be between 11 to 14 digit")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contact no must be numeric")]
        
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please select designation")]
        [DisplayName("Designation")]

        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        [Required(ErrorMessage = "Please select department")]
        [DisplayName("Department")]

        public int DepartmentId { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Enter Course Credit")]
        [Range(0, 40, ErrorMessage = "Invalid number ! please give positive number")]
        [DisplayName("Credit to be taken")]
        public double CreditTake { get; set; }
        public double CreditLeft { get; set; }
        public double Credit { get; set; }
    }
}