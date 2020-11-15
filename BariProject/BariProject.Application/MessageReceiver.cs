using Amazon.SQS.Model;
using BariProject.Domain.Commands;
using BariProject.Domain.Handlers;
using BariProject.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BariProject.Application
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly ICommandHandler<DeleteMessageCommand> _deleteCommandHandler;
        private readonly IQueryHandler<ReceiveMessageQuery, IEnumerable<Message>> _queryHandler;

        private Timer _timer;

        public MessageReceiver(
            ICommandHandler<DeleteMessageCommand> deleteCommandHandler,
            IQueryHandler<ReceiveMessageQuery, IEnumerable<Message>> queryHandler
        )
        {
            _deleteCommandHandler = deleteCommandHandler;
            _queryHandler = queryHandler;
        }

        public event EventHandler<MessageEventArgs> OnReceive;
        public void Start() =>
            _timer = new Timer(
                e => Task.Run(async () => await startReceive()),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5)
           );

        private async Task startReceive()
        {
            var messages = await _queryHandler.Handle(new ReceiveMessageQuery());
            if (OnReceive != null) {
                foreach (var message in messages) {
                    OnReceive.Invoke(this, new MessageEventArgs { Message = message.Body });
                    await _deleteCommandHandler.Handle(new DeleteMessageCommand(message.ReceiptHandle));
                }
            }
        }

        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, 0);
        }
    }
}
