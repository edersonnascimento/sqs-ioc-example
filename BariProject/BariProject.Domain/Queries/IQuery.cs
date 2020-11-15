using BariProject.CrossCutting.SQS;
using System.Threading.Tasks;

namespace BariProject.Domain.Queries
{
    public interface IQuery<T>
    {
        Task<T> Execute(AwsSQSClient client);
    }
}
