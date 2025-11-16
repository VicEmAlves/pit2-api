using System;

namespace Pit2Api.Model.Dto
{
    public class RegisterDto
    {
        public string? NickName { get; set; }
        public string? Senha { get; set; }
        public int IdPerguntaSeguranca { get; set; }
        public string? RespostaPergunta { get; set; }
    }
}