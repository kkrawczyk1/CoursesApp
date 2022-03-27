using Courses.Models;

namespace Courses.Queries
{
    public class GetCourseByIdQuery : BaseQuery<CourseDTO>
    {
        public int CourseId { get; set; }

        public GetCourseByIdQuery(int courseId)
        {
            CourseId = courseId;
        }
    }
}
