using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTM.School.Core.Models;
using LTM.School.Data;
using LTM.School.App.Dtos;
using LTM.School.Common;

namespace LTM.School.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        #region 搜索和排序
        public async Task<IActionResult> Index(string sortOrder, string searchStudent, int? page, string currentStudent)
        {
            #region   

            //姓名的排序参数
            ViewData["Name_Sort_Parm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //时间的排序参数
            ViewData["Date_Sort_Parm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //搜索的关键字
            ViewData["SearchStudent"] = searchStudent;

            #endregion

            ViewData["CurrentSort"] = sortOrder;


            if (searchStudent != null)
                page = 1;
            else
                searchStudent = currentStudent;

            #region 搜索和排序功能

            var students = from student in _context.Students select student;

            if (!string.IsNullOrWhiteSpace(searchStudent))
                students = students.Where(a => a.RealName.Contains(searchStudent));


            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(a => a.RealName);
                    break;

                case "Date":
                    students = students.OrderBy(a => a.EnrollmentDate);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(a => a.EnrollmentDate);
                    break;

                default:
                    students = students.OrderBy(a => a.RealName);
                    break;
            }

            #endregion

            var pageSize = 5;


            var entities = students.AsNoTracking();

            var dtos = await PaginatedList<Student>.CreatepagingAsync(entities, page ?? 1, pageSize);

            return View(dtos);
        }
        #endregion


        #region 学生详情
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(a => a.Enrollments).ThenInclude(e => e.Course).AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        #endregion


        #region 返回创建视图 
        public IActionResult Create()
        {
            return View();
        }
        #endregion


        #region 修改创建方法，防止CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new StudentDto
                    {
                        RealName = dto.RealName,
                        EnrollmentDate = dto.EnrollmentDate
                    };
                    _context.Add(entity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
           
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "无法进行数据的保存，请仔细检查你的数据，是否异常。");
            }

            return View(dto);
        } 
        #endregion

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RealName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
