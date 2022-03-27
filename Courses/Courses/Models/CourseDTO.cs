using System.Collections.Generic;

namespace Courses.Models
{
    public class CourseDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SubjectDTO[] Subjects { get; set; }

    }
}
