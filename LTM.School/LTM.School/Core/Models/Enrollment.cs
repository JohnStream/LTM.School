using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTM.School.App.enumsType;

namespace LTM.School.Core.Models
{
    /// <summary>
    /// 登记信息
    /// </summary>
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public CourseGrade? Grade{get;set;}
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
