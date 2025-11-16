using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Dto;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Implementation
{
    public class GameRepository : IGameRepository
    {
        private readonly ISqlDatabase _db;

        public GameRepository(ISqlDatabase db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Complexidade>> ListComplexidadesAsync()
        {
            return await _db.QueryManyAsync<Complexidade>(Scripts.GetAllComplexidades);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateGameAsync(Jogo jogo)
        {
            var affected = await _db.ExecuteAsync(Scripts.InsertJogo, new
            {
                jogo.Id,
                jogo.IdUsuario,
                jogo.IdComplexidade,
                jogo.Nome,
                jogo.DuracaoMinutos,
                jogo.QtdMinimaJogadores,
                jogo.QtdMaximaJogadores,
                jogo.IdadeMinima
            });

            return affected > 0 ? (true, null) : (false, "Failed to insert jogo.");
        }

        public async Task<bool> UpdateGameAsync(Jogo jogo)
        {
            var affected = await _db.ExecuteAsync(Scripts.UpdateJogo, new
            {
                jogo.Id,
                jogo.IdComplexidade,
                jogo.Nome,
                jogo.DuracaoMinutos,
                jogo.QtdMinimaJogadores,
                jogo.QtdMaximaJogadores,
                jogo.IdadeMinima
            });

            return affected > 0;
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var affected = await _db.ExecuteAsync(Scripts.DeleteJogo, new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<Jogo>> ListGamesByUserAsync(Guid userId)
        {
            return await _db.QueryManyAsync<Jogo>(Scripts.GetGamesByUser, new { IdUsuario = userId });
        }

        public async Task<IEnumerable<Jogo>> ListGamesByUserWithFiltersAsync(Guid userId, GameFilterDto? filters)
        {
            var nomeParam = string.IsNullOrWhiteSpace(filters?.Nome) ? null : $"%{filters!.Nome}%";
            int? duracaoMax = filters?.DuracaoMaxima;
            int? qtdPessoas = filters?.QtdPessoas;
            int? idadeMinima = filters?.IdadeMinima;

            string? complexidade;
            if (filters != null && filters.Complexidade != null && filters.Complexidade.Any())
            {
                complexidade = string.Join(';', filters.Complexidade);
            }
            else
            {
                complexidade = null;
            }

            return await _db.QueryManyAsync<Jogo>(Scripts.GetGamesByUserWithFilters, new
            {
                IdUsuario = userId,
                Nome = nomeParam,
                DuracaoMaxima = duracaoMax,
                QtdPessoas = qtdPessoas,
                IdadeMinima = idadeMinima,
                Complexidade = complexidade
            });
        }
    }
}