using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
    /// <summary>
    /// 登记信息
    /// </summary>
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CouseId { get; set; }
    }
}
