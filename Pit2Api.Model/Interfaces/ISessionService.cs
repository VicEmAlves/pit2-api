using Pit2Api.Model.Dto;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Model.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Secao>> ListSectionsByUserAsync(Guid userId);
        Task<IEnumerable<SessionGameDto>> ListGamesBySectionAsync(Guid sectionId);
        Task<bool> DeleteSectionAsync(Guid id);
        Task<(bool Success, string? ErrorMessage)> CreateSectionAsync(Secao secao);
        Task<(bool Success, string? ErrorMessage)> UpdateSectionGamesAsync(Guid idSecao, IEnumerable<UpdateSectionGamesItemDto> jogos);
    }
}