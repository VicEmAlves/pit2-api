using Pit2Api.Model.Dto;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Model.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Complexidade>> ListComplexidadesAsync();
        Task<(bool Success, string? ErrorMessage)> CreateGameAsync(Jogo jogo);
        Task<bool> UpdateGameAsync(Jogo jogo);
        Task<bool> DeleteGameAsync(Guid id);
        Task<IEnumerable<Jogo>> ListGamesByUserAsync(Guid userId);
        Task<IEnumerable<Jogo>> ListGamesByUserWithFiltersAsync(Guid userId, GameFilterDto? filters);
    }
}