using System;

namespace Pit2Api.Model.Dto
{
    public class CreateSectionDto
    {
        public Guid IdUsuario { get; set; }
        public int IdadeJogadorMaisNovo { get; set; }
        public int DuracaoMinutos { get; set; }
        public int QtdJogadores { get; set; }
        public int NivelComplexidadeMinima { get; set; }
        public int NivelComplexidadeMaxima { get; set; }
    }
}