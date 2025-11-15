using System;

namespace Pit2Api.Model.Models
{
    public class Secao
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public int IdadeJogadorMaisNovo { get; set; }

        public int DuracaoMinutos { get; set; }

        public int QtdJogadores { get; set; }

        public int NivelComplexidadeMinima { get; set; }

        public int NivelComplexidadeMaxima { get; set; }
    }
}