using System.ComponentModel.DataAnnotations;

namespace firstProgram.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name must be between 3 and 50 characters")]
        public string StudentName { get; set; }



        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }


        [Display(Name = "Course")]
        [Required(ErrorMessage = "Course is required")]
        [RegularExpression("^(BCA|MCA|BTECH)$",
        ErrorMessage = "Only BCA, MCA or BTECH are allowed")]
        public string Course { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EnrollmentDate { get; set; }
       
    }
}
