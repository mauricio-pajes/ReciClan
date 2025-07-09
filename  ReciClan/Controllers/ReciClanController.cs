using Microsoft.AspNetCore.Mvc;
using ReciClan.Services;

namespace ReciClan.Controllers;

[ApiController]
[Route("[controller]")]
public class ReciClanController : ControllerBase
{
    private readonly IReciClanService _reciClanService;
    
    public ReciClanController(IReciClanService reciClanService, IResumenService resumenService)
    {
        _reciClanService = reciClanService;
    }

    [HttpGet(Name = "GetReciClanReports")]
    public IEnumerable<ReciClanModel> Get()
    {
        return _reciClanService.Get();
    }
}