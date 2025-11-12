using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos.Calificacion;
using ApiEcommerce.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(Roles = "Votante")]
public class EncuestaController : ControllerBase
{
    private readonly ICalificacionRepository _calificacionRepository;

    public EncuestaController(ICalificacionRepository calificacionRepository)
    {
        _calificacionRepository = calificacionRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost("calificar")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegistrarCalificacion([FromBody] CalificacionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue("id");
        var userName = User.FindFirstValue("username");
        var role = User.FindFirstValue("role");
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("No se pudo identificar al usuario desde el token.");
        }

        var yaExisteCalificacion = await _calificacionRepository.AlreadyRespondedAsync(userName);

        if (yaExisteCalificacion)
        {
            return Conflict(
                new { message = "El usuario ya ha respondido esta encuesta anteriormente." }
            );
        }

        string categoria = dto.CalificacionValor switch
        {
            >= 9 => "Promotor",
            >= 7 => "Neutro",
            _ => "Detractor",
        };

        var nuevaCalificacion = new Calificacion
        {
            CalificacionValor = dto.CalificacionValor,
            UserName = userName,
            Categoria = categoria,
            FechaCreacion = DateTime.UtcNow,
        };

        await _calificacionRepository.AddAsync(nuevaCalificacion);

        return Ok(new { message = "¡Gracias! Su calificación ha sido registrada exitosamente." });
    }

    [HttpGet("yaRespondio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> YaRespondio()
    {
        var userName = User.FindFirstValue("username");

        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized(
                new { message = "No se pudo identificar al usuario desde el token." }
            );
        }

        var yaRespondio = await _calificacionRepository.AlreadyRespondedAsync(userName);

        return Ok(new { yaRespondio });
    }
}
