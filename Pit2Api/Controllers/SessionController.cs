using Microsoft.AspNetCore.Mvc;
using Pit2Api.Model.Dto;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;

namespace Pit2Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
        {
            _service = service;
        }

        // 1 -> Listar todas as seções que um usuário tem
        [HttpGet("user/{userId}/sections")]
        public async Task<IActionResult> ListSectionsByUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var list = await _service.ListSectionsByUserAsync(userId);
            return Ok(list);
        }

        // 2 -> Listar todos os jogos que estão vinculados em uma seção.
        // Caso PrimeiraVez esteja true, aumenta a duração do jogo em 50%
        [HttpGet("{sectionId:guid}/games")]
        public async Task<IActionResult> ListGamesBySection(Guid sectionId)
        {
            if (sectionId == Guid.Empty)
                return BadRequest();

            var list = await _service.ListGamesBySectionAsync(sectionId);
            return Ok(list);
        }

        // 3 -> Deletar Uma Seção
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var ok = await _service.DeleteSectionAsync(id);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        // 4 -> Criar uma nova seção, respeitando MaxSessionsPerUser
        [HttpPost("create")]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionDto dto)
        {
            if (dto is null)
                return BadRequest("Payload required.");

            if (dto.IdUsuario == Guid.Empty)
                return BadRequest("IdUsuario is required.");

            var secao = new Secao
            {
                Id = Guid.NewGuid(),
                IdUsuario = dto.IdUsuario,
                IdadeJogadorMaisNovo = dto.IdadeJogadorMaisNovo,
                DuracaoMinutos = dto.DuracaoMinutos,
                QtdJogadores = dto.QtdJogadores,
                NivelComplexidadeMinima = dto.NivelComplexidadeMinima,
                NivelComplexidadeMaxima = dto.NivelComplexidadeMaxima
            };

            var (success, error) = await _service.CreateSectionAsync(secao);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(ListSectionsByUser), new { userId = secao.IdUsuario }, null);
        }

        // 5 -> Atualizar quantidade de Jogos em uma seção.
        // Valida durações (cada jogo deve ter duração <= max da seção).
        // Se PrimeiraVez, duração do jogo é aumentada em 50% para validação.
        [HttpPost("update-games")]
        public async Task<IActionResult> UpdateSectionGames([FromBody] UpdateSectionGamesDto dto)
        {
            if (dto is null || dto.IdSecao == Guid.Empty)
                return BadRequest("Payload with valid IdSecao required.");

            var (success, error) = await _service.UpdateSectionGamesAsync(dto.IdSecao, dto.Jogos);
            if (!success)
                return BadRequest(error);

            return Ok();
        }
    }
}
