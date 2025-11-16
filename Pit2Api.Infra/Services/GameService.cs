using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Dto;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;

namespace Pit2Api.Infra.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo;
        private readonly ISessionRepository _sessionRepo;

        public GameService(IGameRepository repo, ISessionRepository sessionRepo)
        {
            _repo = repo;
            _sessionRepo = sessionRepo;
        }

        public async Task<IEnumerable<Complexidade>> ListComplexidadesAsync()
            => await _repo.ListComplexidadesAsync();

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

        public async Task<bool> UpdateGameAsync(Jogo jogo)
            => await _repo.UpdateGameAsync(jogo);

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            await _sessionRepo.DeleteGameFromSection(id);
            return await _repo.DeleteGameAsync(id);
        }
        public async Task<IEnumerable<Jogo>> ListGamesByUserAsync(Guid userId)
            => await _repo.ListGamesByUserAsync(userId);

        public async Task<IEnumerable<Jogo>> ListGamesByUserWithFiltersAsync(Guid userId, GameFilterDto? filters)
            => await _repo.ListGamesByUserWithFiltersAsync(userId, filters);
    }
}