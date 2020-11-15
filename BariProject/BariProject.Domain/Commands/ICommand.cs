using BariProject.CrossCutting.SQS;
using System.Threading.Tasks;

namespace BariProject.Domain.Commands
{
    public interface ICommand
    {
        Task Execute(AwsSQSClient client);
    }
}
