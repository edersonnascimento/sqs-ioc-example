using BariProject.Domain.Commands;
using BariProject.Domain.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BariProject.Application
{
    public class MessageSender : IMessageSender
    {
        private readonly ICommandHandler<SendMessageCommand> _commandHandler;

        private Timer _timer;

        public MessageSender(ICommandHandler<SendMessageCommand> commandHandler) => _commandHandler = commandHandler;

        public void Start() =>
            _timer = new Timer(
                e => Task.Run(async () => await startSend()),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5)
           );

        private async Task startSend()
        {
            await _commandHandler.Handle(new SendMessageCommand("Hello World", "BariProjectMS"));
        }

        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, 0);
        }
    }
}
