using System;
using System.Collections.Generic;

namespace Pit2Api.Model.Dto
{
    public class UpdateSectionGamesDto
    {
        public Guid IdSecao { get; set; }
        public List<UpdateSectionGamesItemDto> Jogos { get; set; } = new();
    }
}