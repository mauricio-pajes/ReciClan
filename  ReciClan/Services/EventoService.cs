namespace ReciClan.Services;

public interface IEventoService
{
    IEnumerable<EventoLista> Listar();
    EventoDetalle? Obtener(int id);
    void Registrar(int id, string email);
}

public interface INotificacionService
{
    void EnviarRecordatorio(int eventoId, string email, DateTime fechaHora);
}

public record EventoLista(int Id, string Nombre, DateTime Fecha, string Ubicacion);
public record EventoDetalle(
    int Id, string Nombre, DateTime FechaHora,
    string Ubicacion, string Responsables, string Recomendaciones);

public class EventoService : IEventoService
{
    private readonly INotificacionService _notifs;
    private static readonly List<EventoDetalle> _repo =
    [
        new(1, "Limpieza Playa Costa Verde",
            new DateTime(2025, 8, 1, 9, 0, 0),
            "Playa Agua Dulce", "ONG VerdeMar", "Llevar guantes y agua"),
        new(2, "Recojo de Residuos en Parque Kennedy",
            new DateTime(2025, 8, 5, 8, 30, 0),
            "Miraflores", "Municipalidad", "Bloqueador solar")
    ];

    public EventoService(INotificacionService notifs) => _notifs = notifs;

    public IEnumerable<EventoLista> Listar() =>
        _repo.Select(e => new EventoLista(e.Id, e.Nombre, e.FechaHora.Date, e.Ubicacion));

    public EventoDetalle? Obtener(int id) => _repo.FirstOrDefault(e => e.Id == id);

    public void Registrar(int id, string email)
    {
        var evt = Obtener(id) ??
                  throw new ArgumentException("Evento no encontrado");

        _notifs.EnviarRecordatorio(id, email, evt.FechaHora.AddDays(-1));
    }
}