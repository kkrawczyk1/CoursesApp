using Courses.Models;

namespace Courses.Commands
{
    public class CreateCourseCommand : BaseCommand
    {
        public CourseDTO Course { get; set; }

        public CreateCourseCommand(CourseDTO course)
        {
            Course = course;
        }
    }
}
