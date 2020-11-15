using Amazon.SQS.Model;
using BariProject.CrossCutting.SQS;
using BariProject.Domain.Commands;
using BariProject.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BariProject.Domain.Handlers
{
    public class AwsHandler :
        ICommandHandler<SendMessageCommand>,
        ICommandHandler<DeleteMessageCommand>,
        IQueryHandler<ReceiveMessageQuery, IEnumerable<Message>>
    {
        private readonly AwsSQSClient _awsSqsClient;

        public AwsHandler(AWSSQSConfig config) => _awsSqsClient = new AwsSQSClient(config);

        public async Task Handle(SendMessageCommand command) => await command.Execute(_awsSqsClient);
        public async Task Handle(DeleteMessageCommand command) => await command.Execute(_awsSqsClient);
        public async Task<IEnumerable<Message>> Handle(ReceiveMessageQuery query) => await query.Execute(_awsSqsClient);
    }
}
