using System.Threading;
using System.Threading.Tasks;
using Courses.Commands;
using Courses.Interfaces;
using MediatR;

namespace Courses.CommandHandlers
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _repository;

        public DeleteCourseCommandHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteCourse(request.CourseId);
            return new Unit();
        }
    }
}
