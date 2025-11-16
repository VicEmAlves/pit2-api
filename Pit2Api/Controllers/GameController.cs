using Microsoft.AspNetCore.Mvc;
using Pit2Api.Model.Dto;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pit2Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        // 1 -> Listar Complexidades
        [HttpGet("complexidades")]
        public async Task<IActionResult> ListComplexidades()
        {
            var list = await _service.ListComplexidadesAsync();
            return Ok(list);
        }

        // 2 -> Cadastrar um Jogo novo de acordo com um UsuarioId.
        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto dto)
        {
            if (dto is null)
                return BadRequest("Payload required.");

            if (dto.IdUsuario == Guid.Empty)
                return BadRequest("IdUsuario is required.");

            var jogo = new Jogo
            {
                Id = Guid.NewGuid(),
                IdUsuario = dto.IdUsuario,
                IdComplexidade = dto.IdComplexidade,
                Nome = dto.Nome,
                DuracaoMinutos = dto.DuracaoMinutos,
                QtdMinimaJogadores = dto.QtdMinimaJogadores,
                QtdMaximaJogadores = dto.QtdMaximaJogadores,
                IdadeMinima = dto.IdadeMinima
            };

            var (success, error) = await _service.CreateGameAsync(jogo);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(ListComplexidades), null, null);
        }

        // 3 -> Atualizar os dados de um Jogo já cadastrado
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGame([FromBody] UpdateGameDto dto)
        {
            if (dto is null || dto.Id == Guid.Empty)
                return BadRequest("Payload with valid Id required.");

            var jogo = new Jogo
            {
                Id = dto.Id,
                IdUsuario = dto.IdUsuario,
                IdComplexidade = dto.IdComplexidade,
                Nome = dto.Nome,
                DuracaoMinutos = dto.DuracaoMinutos,
                QtdMinimaJogadores = dto.QtdMinimaJogadores,
                QtdMaximaJogadores = dto.QtdMaximaJogadores,
                IdadeMinima = dto.IdadeMinima
            };

            var ok = await _service.UpdateGameAsync(jogo);
            if (!ok)
                return NotFound();

            return Ok();
        }

        // 4 -> Deletar um Jogo da base
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var ok = await _service.DeleteGameAsync(id);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        // 5 -> Listar todos os jogos que um usuário tem
        [HttpGet("user/{userId:guid}/games")]
        public async Task<IActionResult> ListGamesByUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var list = await _service.ListGamesByUserAsync(userId);
            return Ok(list);
        }

        // 6 -> Listar jogos de um usuário com filtros opcionais (payload). Ignore null filters.
        [HttpPost("user/{userId:guid}/games/search")]
        public async Task<IActionResult> ListGamesByUserWithFilters(Guid userId, [FromBody] GameFilterDto? filters)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            var list = await _service.ListGamesByUserWithFiltersAsync(userId, filters);
            return Ok(list);
        }
    }
}
