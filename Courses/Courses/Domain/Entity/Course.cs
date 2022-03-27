using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses.Domain.Entity
{
    public class Course
    {
        public Course()
        {
            
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }
}
