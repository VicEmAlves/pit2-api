using Microsoft.AspNetCore.Mvc;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;
using Pit2Api.Model.Dto;

namespace Pit2Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        // 1 -> Cadastrar Novo Usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto is null)
                return BadRequest("Payload required.");

            if (string.IsNullOrWhiteSpace(dto.NickName) || string.IsNullOrWhiteSpace(dto.Senha))
                return BadRequest("NickName and Senha are required.");

            var user = new Usuario
            {
                Id = Guid.NewGuid(),
                NickName = dto.NickName,
                IdPerguntaSeguranca = dto.IdPerguntaSeguranca,
                RespostaPergunta = dto.RespostaPergunta,
                Senha = dto.Senha
            };

            var (success, error) = await _service.RegisterAsync(user);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(GetSecurityQuestion), new { nick = user.NickName }, null);
        }

        // 2 -> Buscar Resposta de Seguranca (returns question text)
        [HttpGet("security-question/{nick}")]
        public async Task<IActionResult> GetSecurityQuestion(string nick)
        {
            if (string.IsNullOrWhiteSpace(nick))
                return BadRequest();

            var question = await _service.GetSecurityQuestionAsync(nick);
            if (question is null)
                return NotFound();

            return Ok(question);
        }

        // 3 -> Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.NickName) || string.IsNullOrWhiteSpace(dto.Senha))
                return BadRequest();

            var ok = await _service.LoginAsync(dto.NickName, dto.Senha);
            if (!ok)
                return Unauthorized();

            return Ok();
        }

        // 4 -> Validar Pergunta Segurança
        [HttpPost("validate-security-answer")]
        public async Task<IActionResult> ValidateSecurityAnswer([FromBody] SecurityAnswerDto dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.NickName) || dto.Resposta is null)
                return BadRequest();

            var ok = await _service.ValidateSecurityAnswerAsync(dto.NickName, dto.Resposta);
            return Ok(ok);
        }

        // 5 -> Alterar Senha
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.NickName) || string.IsNullOrWhiteSpace(dto.NovaSenha))
                return BadRequest();

            var ok = await _service.ChangePasswordAsync(dto.NickName, dto.NovaSenha);
            if (!ok)
                return NotFound();

            return Ok();
        }

        // 6 -> Listar Perguntas de Segurança
        [HttpGet("security-questions")]
        public async Task<ActionResult<IEnumerable<PerguntaSeguranca>>> ListSecurityQuestions()
        {
            var list = await _service.ListSecurityQuestionsAsync();
            return Ok(list);
        }
    }
}