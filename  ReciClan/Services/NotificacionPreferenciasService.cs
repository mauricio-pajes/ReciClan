namespace ReciClan.Services;

public interface IPreferenciasService
{
    void Guardar(string email, PreferenciasDto dto);
    PreferenciasDto? Obtener(string email);
}

public record PreferenciasDto(
    string[] TiposAlerta,
    string Frecuencia,
    double RadioKm
);

public class PreferenciasService : IPreferenciasService
{
    private static readonly Dictionary<string, PreferenciasDto> _store = new();

    public void Guardar(string email, PreferenciasDto dto) => _store[email] = dto;
    public PreferenciasDto? Obtener(string email) => _store.TryGetValue(email, out var dto) ? dto : null;
}