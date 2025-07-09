using Microsoft.AspNetCore.Mvc;
using ReciClan.Services;

namespace ReciClan.Controllers;

[ApiController]
[Route("notificaciones")]
public class PreferenciasController : ControllerBase
{
    private readonly IPreferenciasService _svc;
    public PreferenciasController(IPreferenciasService svc) => _svc = svc;

    [HttpPost("preferencias/{email}")]
    public IActionResult Guardar(string email, [FromBody] PreferenciasDto dto)
    {
        if (dto.TiposAlerta.Length == 0 || string.IsNullOrWhiteSpace(dto.Frecuencia))
            return BadRequest("Campos obligatorios");

        _svc.Guardar(email, dto);
        return Ok(new { mensaje = "Preferencias guardadas" });
    }
}