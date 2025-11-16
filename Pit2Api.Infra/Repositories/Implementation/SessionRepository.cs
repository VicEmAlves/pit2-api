using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Implementation
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ISqlDatabase _db;

        public SessionRepository(ISqlDatabase db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Secao>> ListSectionsByUserAsync(Guid userId)
            => await _db.QueryManyAsync<Secao>(Scripts.GetSectionsByUser, new { IdUsuario = userId });

        public async Task<int> CountSectionsByUserAsync(Guid userId)
            => await _db.QueryOneAsync<int>(Scripts.CountSectionsByUser, new { IdUsuario = userId });

        public async Task<int> CreateSectionAsync(Secao secao)
            => await _db.ExecuteAsync(Scripts.InsertSecao, new
            {
                secao.Id,
                secao.IdUsuario,
                secao.IdadeJogadorMaisNovo,
                secao.DuracaoMinutos,
                secao.QtdJogadores,
                secao.NivelComplexidadeMinima,
                secao.NivelComplexidadeMaxima
            });

        public async Task<bool> DeleteSectionAsync(Guid id)
        {
            var affected = await _db.ExecuteAsync(Scripts.DeleteSecao, new { Id = id });
            return affected > 0;
        }

        public async Task<Secao?> GetSectionByIdAsync(Guid id)
            => await _db.QueryOneAsync<Secao?>(Scripts.GetSecaoById, new { Id = id });

        public async Task<IEnumerable<JogoSecaoInfo>> ListGamesBySectionAsync(Guid sectionId)
            => await _db.QueryManyAsync<JogoSecaoInfo>(Scripts.GetGamesBySection, new { IdSecao = sectionId });

        public async Task<Jogo?> GetJogoByIdAsync(Guid id)
            => await _db.QueryOneAsync<Jogo?>(Scripts.GetJogoById, new { Id = id });

        public async Task DeleteGameFromSection(Guid IdJogo)
        {
            var affected = await _db.ExecuteAsync(Scripts.DeleteJogosSecaoByJogo, new { IdJogo = IdJogo });
        }

        public async Task<bool> ReplaceSectionGamesAsync(Guid idSecao, IEnumerable<JogosSecao> jogosSecao)
        {
            // delete existing
            await _db.ExecuteAsync(Scripts.DeleteJogosSecaoBySecao, new { IdSecao = idSecao });

            // insert new ones
            foreach (var js in jogosSecao)
            {
                await _db.ExecuteAsync(Scripts.InsertJogosSecao, new
                {
                    js.IdSecao,
                    js.IdJogo,
                    js.PrimeiraVez
                });
            }

            return true;
        }
    }
}