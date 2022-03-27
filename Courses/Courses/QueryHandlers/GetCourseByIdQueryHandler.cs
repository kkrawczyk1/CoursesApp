using System;
using System.Threading;
using System.Threading.Tasks;
using Courses.Interfaces;
using Courses.Models;
using Courses.Queries;

namespace Courses.QueryHandlers
{
    public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDTO>
    {
        private readonly ICourseRepository _repository;

        public GetCourseByIdQueryHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseDTO> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetById(request.CourseId);
        }
    }
}
