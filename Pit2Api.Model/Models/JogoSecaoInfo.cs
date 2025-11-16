using System;

namespace Pit2Api.Model.Models
{
    public class JogoSecaoInfo
    {
        public Guid IdJogo { get; set; }
        public string? Nome { get; set; }
        public int DuracaoMinutos { get; set; }
        public bool PrimeiraVez { get; set; }
        public Guid IdSecao { get; set; }
    }
}