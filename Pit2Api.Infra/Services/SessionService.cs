using Microsoft.Extensions.Options;
using Pit2Api.Model.Dto;
using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        private readonly Config _config;

        public SessionService(ISessionRepository repo, IOptions<Config> options)
        {
            _repo = repo;
            _config = options.Value;
        }

        public Task<IEnumerable<Secao>> ListSectionsByUserAsync(Guid userId)
            => _repo.ListSectionsByUserAsync(userId);

        public async Task<IEnumerable<SessionGameDto>> ListGamesBySectionAsync(Guid sectionId)
        {
            var list = await _repo.ListGamesBySectionAsync(sectionId);
            var mapped = list.Select(i =>
            {
                var adjusted = i.DuracaoMinutos;
                if (i.PrimeiraVez)
                    adjusted = (int)Math.Ceiling(i.DuracaoMinutos * 1.5);

                return new SessionGameDto
                {
                    IdJogo = i.IdJogo,
                    Nome = i.Nome,
                    DuracaoMinutosOriginal = i.DuracaoMinutos,
                    PrimeiraVez = i.PrimeiraVez,
                    DuracaoMinutosAdjusted = adjusted
                };
            });

            return mapped;
        }

        public Task<bool> DeleteSectionAsync(Guid id)
            => _repo.DeleteSectionAsync(id);

        public async Task<(bool Success, string? ErrorMessage)> CreateSectionAsync(Secao secao)
        {
            if (secao is null)
                return (false, "Payload required.");

            if (secao.IdUsuario == Guid.Empty)
                return (false, "IdUsuario required.");

            var count = await _repo.CountSectionsByUserAsync(secao.IdUsuario);
            if (count >= _config.MaxSessionsPerUser)
                return (false, $"User already has the maximum allowed sections ({_config.MaxSessionsPerUser}).");

            var affected = await _repo.CreateSectionAsync(secao);
            return affected > 0 ? (true, null) : (false, "Failed to create section.");
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateSectionGamesAsync(Guid idSecao, IEnumerable<UpdateSectionGamesItemDto> jogos)
        {
            if (idSecao == Guid.Empty)
                return (false, "IdSecao required.");

            var secao = await _repo.GetSectionByIdAsync(idSecao);
            if (secao is null)
                return (false, "Secao not found.");

            // Validate each jogo duration against section max duration
            var toInsert = new List<JogosSecao>();
            foreach (var item in jogos)
            {
                var jogo = await _repo.GetJogoByIdAsync(item.IdJogo);
                if (jogo is null)
                    return (false, $"Jogo {item.IdJogo} not found.");

                var effective = jogo.DuracaoMinutos;
                if (item.PrimeiraVez)
                    effective = (int)Math.Ceiling(effective * 1.5);

                if (effective > secao.DuracaoMinutos)
                    return (false, $"Jogo '{jogo.Nome}' duration ({effective}) exceeds section max duration ({secao.DuracaoMinutos}).");

                toInsert.Add(new JogosSecao
                {
                    IdSecao = idSecao,
                    IdJogo = item.IdJogo,
                    PrimeiraVez = item.PrimeiraVez
                });
            }

            var ok = await _repo.ReplaceSectionGamesAsync(idSecao, toInsert);
            return ok ? (true, null) : (false, "Failed to update section games.");
        }
    }
}