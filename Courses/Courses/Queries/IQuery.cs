using MediatR;

namespace Courses.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
