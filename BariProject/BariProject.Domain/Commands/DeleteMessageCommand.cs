using BariProject.CrossCutting.SQS;
using System.Threading.Tasks;

namespace BariProject.Domain.Commands
{
    public class DeleteMessageCommand : ICommand
    {
        public DeleteMessageCommand(string receiptHandle) => ReceiptHandle = receiptHandle;
        public string ReceiptHandle { get; private set; }
        public async Task Execute(AwsSQSClient client)
        {
            await client.Delete(ReceiptHandle);
        }
    }
}
