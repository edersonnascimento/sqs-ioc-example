using BariProject.CrossCutting.SQS;
using BariProject.Domain.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BariProject.Domain.Commands
{
    public class SendMessageCommand : ICommand
    {
        public SendMessageCommand(string mensagem, string microServicoId)
        {
            Mensagem = mensagem; 
            MicroServicoId = microServicoId;
        }
        public string Mensagem { get; private set; }
        public string MicroServicoId { get; private set; }
        public async Task Execute(AwsSQSClient client)
        {
            var json = JsonConvert.SerializeObject(new MensagemModel {
                Mensagem = Mensagem,
                MicroServicoId = MicroServicoId
            });

            await client.Send(json);
        }
    }
}
