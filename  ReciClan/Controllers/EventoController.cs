using Microsoft.AspNetCore.Mvc;
using ReciClan.Services;

namespace ReciClan.Controllers;

[ApiController]
[Route("eventos")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _svc;
    public EventoController(IEventoService svc) => _svc = svc;

    [HttpGet]
    public IEnumerable<EventoLista> Listar() => _svc.Listar();

    [HttpGet("{id}")]
    public ActionResult<EventoDetalle> Detalle(int id)
    {
        var det = _svc.Obtener(id);
        return det is null ? NotFound() : det;
    }

    [HttpPost("{id}/registrar")]
    public IActionResult Registrar(int id, [FromQuery] string email)
    {
        _svc.Registrar(id, email);
        return Ok(new { mensaje = "Registro exitoso" });
    }
}