using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;


namespace Pit2Api.Infra.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;

        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(Usuario user)
        {
            if (string.IsNullOrWhiteSpace(user?.NickName))
                return (false, "NickName is required.");

            var exists = await _repo.NickNameExistsAsync(user.NickName);
            if (exists)
                return (false, "NickName already exists.");

            user.Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id;
            await _repo.CreateUserAsync(user);
            return (true, null);
        }

        public Task<string?> GetSecurityQuestionAsync(string nickName)
            => _repo.GetSecurityQuestionByNickAsync(nickName);

        public Task<string> LoginAsync(string nickName, string senhaPlainText)
            => _repo.ValidateLoginAsync(nickName, senhaPlainText);

        public Task<bool> ValidateSecurityAnswerAsync(string nickName, string resposta)
            => _repo.ValidateSecurityAnswerAsync(nickName, resposta);

        public Task<bool> ChangePasswordAsync(string nickName, string novaSenhaPlainText)
            => _repo.ChangePasswordAsync(nickName, novaSenhaPlainText);

        public Task<IEnumerable<PerguntaSeguranca>> ListSecurityQuestionsAsync()
            => _repo.ListSecurityQuestionsAsync();
    }
}
