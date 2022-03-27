using System.Threading.Tasks;

namespace Courses.Interfaces
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
