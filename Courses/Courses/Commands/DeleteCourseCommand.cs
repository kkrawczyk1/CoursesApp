namespace Courses.Commands
{
    public class DeleteCourseCommand : BaseCommand
    {
        public int CourseId { get; set; }

        public DeleteCourseCommand(int courseId)
        {
            CourseId = courseId;
        }
    }
}
