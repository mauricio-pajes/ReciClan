namespace ReciClan.Services;

public interface IZonaService
{
    ZonaRes Crear(ZonaReq req);
    IEnumerable<ZonaRes> Listar();
}

public record ZonaReq(double Lat, double Lng, string Descripcion, string[] Fotos);
public record ZonaRes(int Id, DateTime Fecha, string Descripcion, string[] Fotos);

public class ZonaService : IZonaService
{
    private static readonly List<ZonaRes> _repo = [];
    private static int _nextId = 1;

    public ZonaRes Crear(ZonaReq req)
    {
        var zona = new ZonaRes(
            Id: _nextId++,
            Fecha: DateTime.UtcNow,
            Descripcion: req.Descripcion,
            Fotos: req.Fotos);
        _repo.Add(zona);
        return zona;
    }

    public IEnumerable<ZonaRes> Listar() => _repo;
}