namespace BariProject.CrossCutting.SQS
{
    public class AWSSQSConfig
    {
        public string QueueUrl { get; set; }
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string Endpoint { get; set; }
    }
}
