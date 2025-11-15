using System;

namespace Pit2Api.Model.Models
{
    public class JogosSecao
    {
        public Guid IdSecao { get; set; }

        public Guid IdJogo { get; set; }

        public bool PrimeiraVez { get; set; }
    }
}