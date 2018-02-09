using LTM.School.Data;
using System;
using System.Linq;
using LTM.School.Core.Models;
using LTM.School.App.enumsType;

namespace LTM.School.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            #region 添加种子学生信息

            var students = new[]
            {
                new Student {RealName = "王尼玛", EnrollmentDate = DateTime.Parse("2005-09-01")},
                new Student {RealName = "富贵", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "张全蛋", EnrollmentDate = DateTime.Parse("2003-09-01")},
                new Student {RealName = "叶良辰", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "和珅", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "纪晓岚", EnrollmentDate = DateTime.Parse("2001-09-01")},
                new Student {RealName = "李逍遥", EnrollmentDate = DateTime.Parse("2003-09-01")},
                new Student {RealName = "王小虎", EnrollmentDate = DateTime.Parse("2005-09-01")}
            };
            foreach (var s in students)
                context.Students.Add(s);
            context.SaveChanges();

            #endregion
            #region 添加课程种子信息
            var courses = new[]
            {
                new Course
                {
                    CourseID = 1050,
                    Title = "数学",
                    Credits = 3

                },
                new Course
                {
                    CourseID = 4022,
                    Title = "政治",
                    Credits = 3

                },
                new Course
                {
                    CourseID = 4041,
                    Title = "物理",
                    Credits = 3

                },
                new Course
                {
                    CourseID = 1045,
                    Title = "化学",
                    Credits = 4
                },
                new Course
                {
                    CourseID = 3141,
                    Title = "生物",
                    Credits = 4
                },
                new Course
                {
                    CourseID = 2021,
                    Title = "英语",
                    Credits = 3
                },
                new Course
                {
                    CourseID = 2042,
                    Title = "历史",
                    Credits = 4
                }
            };
            foreach (var c in courses)
                context.Courses.Add(c);
            context.SaveChanges();
            #endregion


           #region 添加登记种子信息
           var enrollments = new[]
            {
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "王尼玛").ID,
                    CourseID = courses.Single(c => c.Title == "数学").CourseID,
                    Grade = CourseGrade.Excellent
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "王尼玛").ID,
                    CourseID = courses.Single(c => c.Title == "政治").CourseID,
                    Grade = CourseGrade.Bad
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "富贵").ID,
                    CourseID = courses.Single(c => c.Title == "物理").CourseID,
                    Grade = CourseGrade.Good
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "富贵").ID,
                    CourseID = courses.Single(c => c.Title == "物理").CourseID,
                    Grade = CourseGrade.Great
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "富贵").ID,
                    CourseID = courses.Single(c => c.Title == "化学").CourseID
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "王尼玛").ID,
                    CourseID = courses.Single(c => c.Title == "生物").CourseID
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "叶良辰").ID,
                    CourseID = courses.Single(c => c.Title == "英语").CourseID,
                    Grade = CourseGrade.Excellent
                }, new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "叶良辰").ID,
                    CourseID = courses.Single(c => c.Title == "历史").CourseID,
                    Grade = CourseGrade.Bad
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "张全蛋").ID,
                    CourseID = courses.Single(c => c.Title == "英语").CourseID,
                    Grade = CourseGrade.Bad
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "张全蛋").ID,
                    CourseID = courses.Single(c => c.Title == "数学").CourseID,
                    Grade = CourseGrade.Bad
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "纪晓岚").ID,
                    CourseID = courses.Single(c => c.Title == "英语").CourseID
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "王小虎").ID,
                    CourseID = courses.Single(c => c.Title == "生物").CourseID
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "和珅").ID,
                    CourseID = courses.Single(c => c.Title == "物理").CourseID,
                    Grade = CourseGrade.Excellent
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.RealName == "和珅").ID,
                    CourseID = courses.Single(c => c.Title == "英语").CourseID
                }
            };
            foreach (var e in enrollments)
                context.Enrollments.Add(e);
            context.SaveChanges();
            #endregion

        }
    }
}