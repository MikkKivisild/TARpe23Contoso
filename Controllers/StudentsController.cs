﻿using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        /*

        public async Task<IActionResult> Index(
            string sordOrder,
            string currentFilter,
            string searchString,
            int? pageNumber
            )
        {
            ViewData["CurrentSort"] = sordOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sordOrder) ? "name_desc":"";
            ViewData["DateSortParam"] = sordOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else 
            {
                searchString = currentFilter;
            }

            ViewData["currentFilter"] =searchString;

            var students = from student in _context.Students
                           select student;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(student =>
                student.LastName.Contains(searchString) || 
                student.FirstName.Contains(searchString));
            }
            switch(sordOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(student => student.LastName);
                    break;

                case "firstname_desc":
                    students = students.OrderByDescending(student => student.FirstName);
                    break;

                case "Date":
                    students = students.OrderBy(student => student.EnrollmentDate);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(student => student.EnrollmentDate);
                    break;

                default:
                    students = students.OrderBy(student => student.LastName);
                    break;
            }
            int pageSize = 3;
            return View(await _context.Students.ToListAsync());
        }
        */
    }
}
