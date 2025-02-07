using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await (schoolContext.ToListAsync()));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * FROM Departments WHERE DepartmentID = {0}";
            var department = await _context.Departments
                                            .FromSqlRaw(query, id)
                                            .Include(d => d.Administrator)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Budget, StartDate, RowVersion, InstructorID, Scholarship")] Department department)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);

        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentToEdit = await _context.Departments
                    .Include(i => i.Administrator)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToEdit == null) { return NotFound(); }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", departmentToEdit.InstructorID);
            return View(departmentToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, byte[] rowVersion)
        {
            ModelState.Remove("rowVersion");
            if (id == null) { return NotFound(); }
            var departmentToUpdate = await _context.Departments.Include(i => i.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToUpdate == null)
            {
                Department departmentIsDeleted = new Department();
                await TryUpdateModelAsync(departmentIsDeleted);
                ModelState.AddModelError(string.Empty, "unable to save changes. Department has already been removed.");
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "LastName", departmentIsDeleted.InstructorID);
                return View(departmentIsDeleted);
            }
            _context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            var tryUpdate = await TryUpdateModelAsync<Department>(departmentToUpdate,
                "",
                s => s.Name,
                s => s.StartDate,
                s => s.Budget,
                s => s.TurkishDepartmentDescription,
                s => s.InstructorID,
                s => s.Scholarship);

            if (tryUpdate)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "unable to save changes, department has already been removed.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();
                        if (databaseValues.Name != clientValues.Name) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}"); }
                        if (databaseValues.StartDate != clientValues.StartDate) { ModelState.AddModelError("Name", $"Current value: {databaseValues.StartDate}"); }
                        if (databaseValues.Budget != clientValues.Budget) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Budget}"); }
                        if (databaseValues.Scholarship != clientValues.Scholarship) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Scholarship}"); }
                        if (databaseValues.TurkishDepartmentDescription != clientValues.TurkishDepartmentDescription) { ModelState.AddModelError("Name", $"Current value: {databaseValues.TurkishDepartmentDescription}"); }
                        if (databaseValues.InstructorID != clientValues.InstructorID) { ModelState.AddModelError("Name", $"Current value: {databaseValues.InstructorID}"); }
                        {
                            Instructor databaseHasThisInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.InstructorID}");
                        }
                        ModelState.AddModelError(string.Empty, "warning, changes you are about to save differ from the info in the DB" + "It appears this department was already" +
                            "changed after you selected the version with the old info." +
                            "click back if this new info is already correct, otherwise, click save again to oversave the department anyways.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }

                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "Fullname", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }
        [HttpGet]
        public async Task<IActionResult> BaseOn(int? id)
        {
            if (id == null)
            { 
                return NotFound();
            }
            var department = await _context.Departments
                .Include(d => d.Administrator)
                .FirstOrDefaultAsync(m =>m.DepartmentID== id);

            if (department == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Make([Bind("InstructorID,Name,Budget,StartDate,TurkishDepartmentDescription")] Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeDelete([Bind("InstructorID,Name,Budget,StartDate,TurkishDepartmentDescription")] Department department)
        {
            _context.Departments.Remove(department);
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
