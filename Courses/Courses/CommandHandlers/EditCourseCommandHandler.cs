using System.Threading;
using System.Threading.Tasks;
using Courses.Commands;
using Courses.Interfaces;
using MediatR;

namespace Courses.CommandHandlers
{
    public class EditCourseCommandHandler : ICommandHandler<EditCourseCommand>
    {
        private readonly ICourseRepository _repository;

        public EditCourseCommandHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            await _repository.EditCourse(request.CourseId, request.Course);
            return new Unit();
        }
    }
}
