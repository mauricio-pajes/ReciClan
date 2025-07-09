namespace ReciClan;

public class ReciClanModel
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public string Descripcion { get; set; } = default!;
    public string? Categoria { get; set; }
}