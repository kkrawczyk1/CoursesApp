using System.Threading.Tasks;
using Courses.Models;

namespace Courses.Interfaces
{
    public interface ICourseRepository
    {
        Task AddCourse(CourseDTO course);
        Task<CourseDTO> GetById(int id);
        Task<CourseDTO[]> GetAll();
        Task DeleteCourse(int id);
        Task EditCourse(int courseId, CourseDTO course);
    }
}
