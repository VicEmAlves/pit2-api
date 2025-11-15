using System;

namespace Pit2Api.Model.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string? NickName { get; set; }

        public string? Senha { get; set; }

        public int IdPerguntaSeguranca { get; set; }

        public string? RespostaPergunta { get; set; }
    }
}