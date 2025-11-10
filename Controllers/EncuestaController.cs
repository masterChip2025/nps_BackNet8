using ApiEcommerce.Models.Dtos.Calificacion;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;


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

        // var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        // var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var userId = User.FindFirstValue("id");
        var userName = User.FindFirstValue("username"); 
        var role = User.FindFirstValue("role"); 
        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("No se pudo identificar al usuario desde el token.");
        }

        var yaExisteCalificacion =  _calificacionRepository.AlreadyRespondedAsync(userName);

        if (yaExisteCalificacion)
        {
            return Conflict(new { message = "El usuario ya ha respondido esta encuesta anteriormente." });
        }

        var nuevaCalificacion = new Calificacion
        {
            CalificacionValor = dto.CalificacionValor,
            UserName = userName,
            FechaCreacion = DateTime.UtcNow
        };

        await _calificacionRepository.AddAsync(nuevaCalificacion);
        // await _context.SaveChangesAsync();

        return Ok(new { message = "¡Gracias! Su calificación ha sido registrada exitosamente." });
    }
}
