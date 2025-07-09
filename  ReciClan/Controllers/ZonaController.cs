using Microsoft.AspNetCore.Mvc;
using ReciClan.Services;

namespace ReciClan.Controllers;


[ApiController]
[Route("zonas")]
public class ZonaController : ControllerBase
{
    private readonly IZonaService _zonas;
    public ZonaController(IZonaService zonas) => _zonas = zonas;

    [HttpPost]
    public IActionResult Crear([FromBody] ZonaReq req)
        => Created("", _zonas.Crear(req));

    [HttpGet]
    public IEnumerable<ZonaRes> Listar() => _zonas.Listar();
}