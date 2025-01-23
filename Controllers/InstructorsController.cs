using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context; 
        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <IActionResult> Index(int? id, int? courseId)
        {
            var vm = new InstructorIndexData();
            vm.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = vm.Instructors
                    .Where(i => i.ID == id.Value).Single();
                vm.Courses = instructor.CourseAssignments
                    .Select(i => i.Course);
            }
            if (courseId != null)
            {
                ViewData["CourseID"] = courseId.Value;
                vm.Enrollments = vm.Courses
                    .Where(x => x.CourseID == courseId)
                    .Single()
                    .Enrollments;
            }

            return View(vm);

        }
        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if(ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }


        public async Task<ActionResult> Edit([Bind("ID,LastName,FirstName,HireDate,City")] Instructor Instructor)
        {
            if(ModelState.IsValid)
            {
                _context.Instructors.Update(Instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Instructor);
        }


        public async Task<ActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);
        
            var InstructorClone = new Instructor();
            InstructorClone.LastName = Instructor.LastName;
            InstructorClone.FirstName = Instructor.FirstName;
            InstructorClone.HireDate = Instructor.HireDate;
            InstructorClone.City = Instructor.City;
        
        
        
            if (ModelState.IsValid)
            {
                _context.Instructors.Add(InstructorClone);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses;
            var instructorCourses = new HashSet<int>(collection: instructor.CourseAssignments.Select(c => c.CourseID));
            var vm = new List<AssignedCourseData>(); 
            foreach (var course in allCourses)
            {
                vm.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = vm;
        }
    }
}
