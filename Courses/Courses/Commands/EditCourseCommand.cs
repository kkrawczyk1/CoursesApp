using Courses.Models;

namespace Courses.Commands
{
    public class EditCourseCommand : BaseCommand
    {
        public int CourseId { get; set; }
        public CourseDTO Course { get; set; }

        public EditCourseCommand(int courseId, CourseDTO course)
        {
            CourseId = courseId;
            Course = course;
        }
    }
}