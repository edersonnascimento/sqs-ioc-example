using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace BariProject.CrossCutting.SQS
{
    public class AWSSettings
    {
        private static AWSSettings _settings;

        public AWSSQSConfig SQSConfig { get; private set; }

        public static AWSSettings Instance
        {
            get
            {
                if (_settings == null) {
                    var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    var builder = new ConfigurationBuilder()
                                    .SetBasePath(currentDirectory)
                                    .AddJsonFile("awsSettings.json", optional: true, reloadOnChange: true);

                    var configuration = builder.Build();

                    _settings = new AWSSettings {
                        SQSConfig = configuration.GetSection("AWSSQSConfig").Get<AWSSQSConfig>()
                    };
                }

                return _settings;
            }
        }
    }
}
