using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LTM.School.ViewModle
{
    public class EnrollmentDateGroup
    {
        [DisplayName("学生总数")]
        public int StudentCount { get; set; }
        [DisplayName("学生注册日期")]
        // 显示到天
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }
    }
}
