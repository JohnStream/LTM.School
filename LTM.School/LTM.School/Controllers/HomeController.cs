using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LTM.School.Data;
using LTM.School.ViewModle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTM.School.Controllers
{
    public class HomeController : Controller
    {

        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "学生统计信息";


            var groups = new List<EnrollmentDateGroup>();


            var conn = _context.Database.GetDbConnection();


            try
            {
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    string sqlQuery = @"select  EnrollmentDate,  COUNT(*) as StudentCount     from Student  group by EnrollmentDate";

                    command.CommandText = sqlQuery;

                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new EnrollmentDateGroup()
                            {
                                EnrollmentDate = reader.GetDateTime(0),
                                StudentCount = reader.GetInt32(1)
                            };
                            groups.Add(row);

                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View(groups);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}