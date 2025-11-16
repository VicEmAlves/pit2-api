using System;

namespace Pit2Api.Model.Dto
{
    public class SessionGameDto
    {
        public Guid IdJogo { get; set; }
        public string? Nome { get; set; }
        public int DuracaoMinutosOriginal { get; set; }
        public bool PrimeiraVez { get; set; }
        public int DuracaoMinutosAdjusted { get; set; }
    }
}