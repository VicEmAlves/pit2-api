using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Abstraction
{
    public interface IGameRepository
    {
        Task<IEnumerable<Complexidade>> ListComplexidadesAsync();
        Task<(bool Success, string? ErrorMessage)> CreateGameAsync(Jogo jogo);
        Task<bool> UpdateGameAsync(Jogo jogo);
        Task<bool> DeleteGameAsync(Guid id);
    }
}