namespace ReciClan.Services;

public interface IResumenService
{
    string[] GetSummaries();
    int GetLength();
}

public class ResumenService : IResumenService
{
    private static readonly string[] _summaries =
    {
        "Plástico", "Orgánico", "Vidrio", "Metal", "Papel",
        "Mixto", "Electrónicos", "Textil", "Madera", "Otros"
    };

    public string[] GetSummaries() => _summaries;
    public int GetLength() => _summaries.Length;
}