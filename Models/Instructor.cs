using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "FullName")]
        public string FullName 
        {
            get
            { return LastName + "," + FirstName; }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Hired on:")]
        public DateTime HireDate { get; set; }
        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }

        public Gender Genderr { get; set; }
        
        public int Age { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }

        public enum Gender
        {
            Male, female
        }
    }
}