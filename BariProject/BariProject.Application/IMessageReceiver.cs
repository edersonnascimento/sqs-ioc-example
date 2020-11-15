using System;

namespace BariProject.Application
{
    public interface IMessageReceiver
    {
        event EventHandler<MessageEventArgs> OnReceive;

        void Start();
        void Stop();
    }
}