using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int IntructorID { get; set; }
        public Instructor? Instructor { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }
    }
}

