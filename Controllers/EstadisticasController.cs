using ApiEcommerce.Repository;
using ApiEcommerce.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class EstadisticasController : ControllerBase
{
    private readonly ICalificacionRepository _calificacionRepository;

    public EstadisticasController(ICalificacionRepository calificacionRepository)
    {
        _calificacionRepository = calificacionRepository;
    }

    [HttpGet("nps")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ObtenerEstadisticas()
    {
        Console.WriteLine("📊 Consultando estadísticas de calificaciones...");
        var estadisticas = await _calificacionRepository.ObtenerEstadisticasAsync();
        return Ok(estadisticas);
    }
}
