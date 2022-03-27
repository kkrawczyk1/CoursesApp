using Courses.Interfaces;
using MediatR;

namespace Courses.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}
