using System;

namespace Pit2Api.Model.Dto
{
    public class GameFilterDto
    {
        public string? Nome { get; set; }
        public int? DuracaoMaxima { get; set; }
        public int? QtdPessoas { get; set; }
        public int? IdadeMinima { get; set; }
        public List<int>? Complexidade { get; set; }
    }
}