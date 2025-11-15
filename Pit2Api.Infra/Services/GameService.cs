using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo;

        public GameService(IGameRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Complexidade>> ListComplexidadesAsync()
            => _repo.ListComplexidadesAsync();

        public async Task<(bool Success, string? ErrorMessage)> CreateGameAsync(Jogo jogo)
        {
            if (jogo is null)
                return (false, "Payload required.");

            if (jogo.IdUsuario == Guid.Empty)
                return (false, "IdUsuario is required.");

            if (string.IsNullOrWhiteSpace(jogo.Nome))
                return (false, "Nome is required.");

            jogo.Id = jogo.Id == Guid.Empty ? Guid.NewGuid() : jogo.Id;

            var (success, error) = await _repo.CreateGameAsync(jogo);
            return (success, error);
        }

        public Task<bool> UpdateGameAsync(Jogo jogo)
            => _repo.UpdateGameAsync(jogo);

        public Task<bool> DeleteGameAsync(Guid id)
            => _repo.DeleteGameAsync(id);
    }
}