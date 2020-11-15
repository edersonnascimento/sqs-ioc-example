using BariProject.Domain.Commands;
using System.Threading.Tasks;

namespace BariProject.Domain.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }
}
