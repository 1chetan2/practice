using System.ComponentModel.DataAnnotations;

namespace firstProgram.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
