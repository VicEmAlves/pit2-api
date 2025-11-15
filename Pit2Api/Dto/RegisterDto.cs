using System;

namespace Pit2Api.Controllers.Dto
{
    public class RegisterDto
    {
        public string? NickName { get; set; }
        public string? Senha { get; set; }
        public int IdPerguntaSeguranca { get; set; }
        public string? RespostaPergunta { get; set; }
    }
}