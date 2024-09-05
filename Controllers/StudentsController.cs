using ContosoUniversity.Data;
using ContosoUniversity.Models;
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

        //  get all for index, retrive all students 
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

        //create get, haarab vaatest andmed, mida create meetod vajab.
        public IActionResult Create(int id)
        {
            return View();
        }

        // create meetod, sisestab andmebaasi uue õpilase, insert new student into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        // Delete meetod, otsib andmebaasist kaasaantud id järgi õpilast.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)// kui id on tühi/null, siis õpilast ei leita
            {
                return NotFound();
            }

            var student = await _context.Students // tehakse õpilase objekt andmebaasis oleva id järgi
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)// kui student objekt on tühi/null, siis ka õpilast ei leita
            {
                return NotFound();
            }
            return View(student);
        }
        //Delete POST meetod, teostab andembaasis vajaliku muudatuse. ehk kustutab andme ära
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id); //otsime andmebaasist õpilast id järgi

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)// kui id on tühi/null, siis õpilast ei leita
            {
                return NotFound();
            }
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        public async Task<ActionResult> Edit([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
             if(ModelState.IsValid)
             {
                 _context.Students.Update(student);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
            return View(student);
        }
    }
}
