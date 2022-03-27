using System.Threading;
using System.Threading.Tasks;
using Courses.Interfaces;
using Courses.Models;
using Courses.Queries;

namespace Courses.QueryHandlers
{
    public class GetCoursesQueryHandler : IQueryHandler<GetCoursesQuery, CourseDTO[]>
    {
        private readonly ICourseRepository _repository;

        public GetCoursesQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseDTO[]> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }
    }
}