using System.Threading;
using System.Threading.Tasks;
using Courses.Commands;
using Courses.Interfaces;
using MediatR;

namespace Courses.CommandHandlers
{
    public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand>
    {
        private readonly ICourseRepository _repository;

        public CreateCourseCommandHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddCourse(request.Course);
            return new Unit();
        }
    }
}
