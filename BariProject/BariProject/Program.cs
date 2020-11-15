using BariProject.Application;
using BariProject.CrossCutting.IoC;
using BariProject.Domain.Commands;
using BariProject.Domain.Handlers;
using IoC;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BariProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var resolver = DependencyConfiguration.Resolver;

            bool send = args.Contains("--send"),
                 receive = args.Contains("--receive");

            if (send) {
                var sender = resolver.Resolve<IMessageSender>();
                sender.Start();
            }

            if (receive) {
                var receiver = resolver.Resolve<IMessageReceiver>();
                receiver.OnReceive += Receiver_OnReceive;
                receiver.Start();
            }

            Console.ReadLine();
        }

        private static void Receiver_OnReceive(object sender, MessageEventArgs e)
        {
            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss:zzzz} | {1}", DateTime.Now,e.Message);
            Console.WriteLine();
        }
    }
}
