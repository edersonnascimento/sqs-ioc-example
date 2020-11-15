using Amazon.SQS.Model;
using BariProject.CrossCutting.SQS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BariProject.Domain.Queries
{
    public class ReceiveMessageQuery : IQuery<IEnumerable<Message>>
    {
        public async Task<IEnumerable<Message>> Execute(AwsSQSClient client)
        {
            return await client.Receive();
        }
    }
}
