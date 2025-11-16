using System;

namespace Pit2Api.Model.Dto
{
    public class UpdateGameDto
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdComplexidade { get; set; }
        public string? Nome { get; set; }
        public int DuracaoMinutos { get; set; }
        public int QtdMinimaJogadores { get; set; }
        public int QtdMaximaJogadores { get; set; }
        public int IdadeMinima { get; set; }
    }
}