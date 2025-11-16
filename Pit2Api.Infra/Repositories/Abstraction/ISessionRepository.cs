using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Abstraction
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Secao>> ListSectionsByUserAsync(Guid userId);
        Task<int> CountSectionsByUserAsync(Guid userId);
        Task<int> CreateSectionAsync(Secao secao);
        Task<bool> DeleteSectionAsync(Guid id);
        Task DeleteGameFromSection(Guid IdJogo);

        Task<Secao?> GetSectionByIdAsync(Guid id);

        Task<IEnumerable<JogoSecaoInfo>> ListGamesBySectionAsync(Guid sectionId);
        Task<Jogo?> GetJogoByIdAsync(Guid id);

        Task<bool> ReplaceSectionGamesAsync(Guid idSecao, IEnumerable<JogosSecao> jogosSecao);
    }
}