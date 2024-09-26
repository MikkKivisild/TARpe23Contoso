using ContosoUniversity.Models;
using Microsoft.Identity.Client;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            //teeb kindlaks et andmebaas tehakse, või oleks olemas
            context.Database.EnsureCreated();

            //kui õpilase tabelis juba on õpilasi, väljub fuktsioonist
            if (context.Students.Any())
            {
                return;
            }
            //objekt õpilastega, mis lisatakse siis, kui õpilasi sisestaud ei ole.
            var students = new Student[]
            {
                new Student{FirstName="Artjom", LastName="Skatškov", EnrollmentDate=DateTime.Parse("2069-04-20")},
                new Student{FirstName="Meredith", LastName="Alonso", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Arturo",LastName="Anand", EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Gytis",LastName="Barzdukas", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Yan",LastName="Li", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Peggy",LastName="Justice", EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstName="Laura",LastName="Norman", EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstName="Nino",LastName="Olivetto", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Carson",LastName="Alexander", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Mikk",LastName="Kivisild", EnrollmentDate=DateTime.Parse("2727-07-27")}
            };
            //iga õpilane lisatakse ükshaaval läbi foreach tsükli
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            //ja andmebaasi muudatused salvestatakse
            context.SaveChanges();

            //eelnev struktuur, kui kursustega
            var couses = new Course[]
                {
                    new Course{CourseID=1050, Title="Keemia", Credits=3},
                    new Course{CourseID=4022, Title="Mikroökonoomika", Credits=3},
                    new Course{CourseID=4021, Title="Mikroökonoomika", Credits=3},
                    new Course{CourseID=1045, Title="Calculus", Credits=4},
                    new Course{CourseID=3141, Title="Trigonomeetria", Credits=4},
                    new Course{CourseID=2021, Title="Kompositsioon", Credits=3},
                    new Course{CourseID=2042, Title="Kirjandus", Credits=4},
                    new Course{CourseID=6789, Title="Plaadilugeja", Credits=1}
                };
            foreach (Course course in couses) 
            {
                context.SaveChanges();
            }

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1, CourseID=1050, Grade=Grade.A},
                new Enrollment{StudentID=1, CourseID=1050, Grade=Grade.B},
                new Enrollment{StudentID=1, CourseID=4022, Grade=Grade.C},

                new Enrollment{StudentID=2, CourseID=4022, Grade=Grade. F},
                new Enrollment{StudentID=2, CourseID=4021, Grade=Grade.C},
                new Enrollment{StudentID=2, CourseID=4021, Grade=Grade.B},

                new Enrollment{StudentID=8, CourseID=1045, Grade=Grade.F},

                new Enrollment{StudentID=3, CourseID=1045},
                
                new Enrollment{StudentID=4, CourseID=3141, Grade=Grade.B},
                
                new Enrollment{StudentID=5, CourseID=3141, Grade=Grade.A},
                
                new Enrollment{StudentID=7, CourseID=2021, Grade=Grade.B},
                
                new Enrollment{StudentID=6, CourseID=2021},
                
                new Enrollment{StudentID=8, CourseID=2042, Grade=Grade.C},
                
                new Enrollment{StudentID=9, CourseID=2042, Grade=Grade.A},
                
                new Enrollment{StudentID=10, CourseID=6789, Grade=Grade.A},
            };
            foreach (Enrollment enrollment in enrollments)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();

            //siit palju puudu ;-; nii et AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA


            if (context.Departments.Any()) { return; }
            var departments = new Department[]
            {
                new Department
                {
                    Name = "InfoTech",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-09-01"),
                    TurkishDepartmentDescription = "Skibidi tuvaleti. Skibidi tuvalet Skibidi tuvalet. Skibidi tuvalet Skibidi tuvalet.",
                    InstructorID = 1
                },
                new Department
                {
                    Name = "Skibidi toilet",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-04-09"),
                    TurkishDepartmentDescription = "nçok usta skibidi tuvalet.",
                    InstructorID = 2
                },
                new Department
                {
                    Name = "Skibidi sigma",
                    Budget = 0,
                    StartDate = DateTime.Parse("2024-02-03"),
                    TurkishDepartmentDescription = "skibidi tuvaleti çok sigma."
                }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }
    }
}












































