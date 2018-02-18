using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LTM.School.Core.Models
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public int ID { get; set; }
        [DisplayName("姓名")]
        public string RealName{get;set;}
        [DisplayName("注册时间")]
        public DateTime EnrollmentDate { get; set; }
        [DisplayName("课程详情")]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
