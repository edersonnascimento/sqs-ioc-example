using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BariProject.CrossCutting.SQS
{
    public class AwsSQSClient : IAwsSQSClient
    {
        private readonly string _queueUrl;
        private readonly IAmazonSQS _sqsClient;

        public AwsSQSClient(AWSSQSConfig config)
        {
            _sqsClient = new AmazonSQSClient(
                config.AwsAccessKeyId,
                config.AwsSecretAccessKey,
                RegionEndpoint.GetBySystemName(config.Endpoint)
            );
            _queueUrl = config.QueueUrl;
        }

        public async Task Send(string message) => await _sqsClient.SendMessageAsync(new SendMessageRequest {
            QueueUrl = _queueUrl,
            MessageBody = message,
            MessageGroupId = "Alive"
        });
        public async Task<IEnumerable<Message>> Receive()
        {
            var request = new ReceiveMessageRequest {
                QueueUrl = _queueUrl
            };

            StringBuilder builder = new StringBuilder();
            var response = await _sqsClient.ReceiveMessageAsync(request);

            return response.Messages;
            //foreach (var message in response.Messages) {
            //    builder.AppendLine("Message Info:");
            //    builder.AppendLine($"\tMessageId: {message.MessageId}");
            //    builder.AppendLine($"\tReceiptHandle: {message.ReceiptHandle}");
            //    builder.AppendLine($"\tMD5OfBody: {message.MD5OfBody}");
            //    builder.AppendLine($"\tBody: {message.Body}");
            //    builder.AppendLine("--------------------------------");

            //    foreach (KeyValuePair<string, string> entry in message.Attributes) {
            //        builder.AppendLine("  Attribute");
            //        builder.AppendLine($"    Name: {entry.Key};  Value: {entry.Value}");
            //    }

            //    await Delete(message);
            //}
            //return builder.ToString();
        }
        public async Task Delete(ReceiveMessageResponse response)
        {
            foreach (var message in response.Messages) {
                await Delete(message);
            }
        }
        public async Task Delete(Message message) => await Delete(message.ReceiptHandle);
        public async Task Delete(string receiptHandle)
        {
            var request = new DeleteMessageRequest {
                QueueUrl = _queueUrl,
                ReceiptHandle = receiptHandle
            };
            await _sqsClient.DeleteMessageAsync(request);
        }
    }
}
