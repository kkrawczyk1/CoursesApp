using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Domain.Entity
{
    public class Subject
    {
        public Subject()
        {

        }

        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(40)]
        public string SubjectName { get; set; }
        public int SubjectNumber { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
