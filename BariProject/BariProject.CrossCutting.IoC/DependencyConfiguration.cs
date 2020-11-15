using Amazon.SQS.Model;
using BariProject.Application;
using BariProject.CrossCutting.SQS;
using BariProject.Domain.Commands;
using BariProject.Domain.Handlers;
using BariProject.Domain.Queries;
using IoC;
using System.Collections.Generic;

namespace BariProject.CrossCutting.IoC
{
    public class DependencyConfiguration : IConfiguration
    {
        public static IMutableContainer Resolver => Container.Create().Using<DependencyConfiguration>();
        public IEnumerable<IToken> Apply([NotNull] IMutableContainer container)
        {
            var setting = AWSSettings.Instance;

            yield return container
                .Bind<ICommandHandler<SendMessageCommand>>()
                .Bind<ICommandHandler<DeleteMessageCommand>>()
                .Bind<IQueryHandler<ReceiveMessageQuery, IEnumerable<Message>>>()
                    .To<AwsHandler>(ctx => new AwsHandler(setting.SQSConfig));

            yield return container
                .Bind<IMessageSender>().To<MessageSender>()
                .Bind<IMessageReceiver>().To<MessageReceiver>();
        }
    }
}
