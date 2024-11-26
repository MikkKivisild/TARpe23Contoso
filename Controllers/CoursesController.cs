using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {

        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }


        //create ja edit bs
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] Course course)
        {
            if (!ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var CourseId = await _context.Courses.MaxAsync(m => (int?)m.CourseID) ?? 0;
            course.CourseID = CourseId + 1;
            return View(course);
        }
        public async Task<ActionResult> Edit([Bind("CourseID,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }





        //details, delete ja siis veel clone  bs
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) 
            {
            return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);

            var courseClone = new Course();
            courseClone.CourseID = course.CourseID;
            courseClone.Title = course.Title;
            courseClone.Credits = course.Credits;

            if (ModelState.IsValid)
            {
                _context.Courses.Add(courseClone);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
