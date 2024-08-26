using ContosoUniversity.Models;
using Microsoft.Identity.Client;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if(context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student{FirstName="Artkom", LastName="Skatškov", EnrollmentDate=DateTime.Parse
                ("2069-04-20")}
            };
        }
    }
}












































