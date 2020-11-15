using Amazon.SQS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BariProject.CrossCutting.SQS
{
    public interface IAwsSQSClient
    {
        Task Delete(ReceiveMessageResponse response);
        Task<IEnumerable<Message>> Receive();
        Task Send(string message);
    }
}