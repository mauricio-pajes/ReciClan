namespace ReciClan.Services;

public interface IReciClanService
{
    ReciClanModel[] Get();
}

public class ReciClanService : IReciClanService
{
    private readonly IResumenService _resumenService;

    public ReciClanService(IResumenService resumenService)
    {
        _resumenService = resumenService;
    }

    public ReciClanModel[] Get()
    {
        var max = _resumenService.GetLength();
        var resumen = _resumenService
            .GetSummaries()[Random.Shared.Next(max)];

        return Enumerable.Range(1, 5).Select(i => new ReciClanModel
        {
            Id = i,
            Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
            Descripcion = $"Zona contaminada #{i}",
            Categoria = resumen
        }).ToArray();
    }
}