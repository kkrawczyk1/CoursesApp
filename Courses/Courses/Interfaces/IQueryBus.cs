using System.Threading.Tasks;
using Courses.Queries;

namespace Courses.Interfaces
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}
