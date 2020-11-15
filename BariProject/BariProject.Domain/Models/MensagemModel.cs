using System;
using System.Collections.Generic;
using System.Text;

namespace BariProject.Domain.Models
{
    public class MensagemModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long Timestamp { get; set; } = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        public string MicroServicoId { get; set; }
        public string Mensagem { get; set; }
    }
}
