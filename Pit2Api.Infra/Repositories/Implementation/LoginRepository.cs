using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Models;

namespace Pit2Api.Infra.Repositories.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ISqlDatabase _db;

        public LoginRepository(ISqlDatabase db)
        {
            _db = db;
        }

        public async Task<bool> NickNameExistsAsync(string nickName)
        {
            var exists = await _db.QueryOneAsync<int>(
                Scripts.NickNameExists,
                new { NickName = (nickName ?? string.Empty).ToUpperInvariant() }
            );
            return exists == 1;
        }

        public async Task CreateUserAsync(Usuario user)
        {
            await _db.ExecuteAsync(
                Scripts.InsertUsuario,
                new
                {
                    user.Id,
                    NickName = (user.NickName ?? string.Empty).ToUpperInvariant(),
                    Senha = user.Senha,
                    user.IdPerguntaSeguranca,
                    RespostaPergunta = (user.RespostaPergunta ?? string.Empty).ToUpperInvariant()
                }
            );
        }

        public async Task<string?> GetSecurityQuestionByNickAsync(string nickName)
        {
            return await _db.QueryOneAsync<string?>(
                Scripts.GetSecurityQuestionByNick,
                new { NickName = (nickName ?? string.Empty).ToUpperInvariant() }
            );
        }

        public async Task<string> ValidateLoginAsync(string nickName, string senhaPlainText)
        {
            var ok = await _db.QueryOneAsync<Guid?>(
                Scripts.ValidateLogin,
                new
                {
                    NickName = (nickName ?? string.Empty).ToUpperInvariant(),
                    Senha = senhaPlainText
                }
            );
            return ok?.ToString() ?? string.Empty;
        }

        public async Task<bool> ValidateSecurityAnswerAsync(string nickName, string resposta)
        {
            var ok = await _db.QueryOneAsync<int>(
                Scripts.ValidateSecurityAnswer,
                new
                {
                    NickName = (nickName ?? string.Empty).ToUpperInvariant(),
                    Resposta = (resposta ?? string.Empty).ToUpperInvariant()
                }
            );
            return ok == 1;
        }

        public async Task<bool> ChangePasswordAsync(string nickName, string novaSenhaPlainText)
        {
            var affected = await _db.ExecuteAsync(
                Scripts.ChangePassword,
                new
                {
                    NickName = (nickName ?? string.Empty).ToUpperInvariant(),
                    Senha = novaSenhaPlainText
                }
            );
            return affected > 0;
        }

        public async Task<IEnumerable<PerguntaSeguranca>> ListSecurityQuestionsAsync()
        {
            return await _db.QueryManyAsync<PerguntaSeguranca>(Scripts.GetAllSecurityQuestions);
        }
    }
}
