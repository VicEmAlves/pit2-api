using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Model.Interfaces
{
    public interface ILoginService
    {
        Task<(bool Success, string? ErrorMessage)> RegisterAsync(Usuario user);
        Task<string?> GetSecurityQuestionAsync(string nickName);
        Task<string> LoginAsync(string nickName, string senhaPlainText);
        Task<bool> ValidateSecurityAnswerAsync(string nickName, string resposta);
        Task<bool> ChangePasswordAsync(string nickName, string novaSenhaPlainText);
        Task<IEnumerable<PerguntaSeguranca>> ListSecurityQuestionsAsync();
    }
}
