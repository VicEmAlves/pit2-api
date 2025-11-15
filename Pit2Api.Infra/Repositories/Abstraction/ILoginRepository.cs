using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Abstraction
{
    public interface ILoginRepository
    {
        Task<bool> NickNameExistsAsync(string nickName);
        Task CreateUserAsync(Usuario user);
        Task<string?> GetSecurityQuestionByNickAsync(string nickName);
        Task<bool> ValidateLoginAsync(string nickName, string senhaPlainText);
        Task<bool> ValidateSecurityAnswerAsync(string nickName, string resposta);
        Task<bool> ChangePasswordAsync(string nickName, string novaSenhaPlainText);
        Task<IEnumerable<PerguntaSeguranca>> ListSecurityQuestionsAsync();
    }
}
