using BariProject.Domain.Queries;
using System.Threading.Tasks;

namespace BariProject.Domain.Handlers
{
    public interface IQueryHandler<T, U> where T : IQuery<U>
    {
        Task<U> Handle(T query);
    }
}
