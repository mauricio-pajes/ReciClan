using Moq;
using ReciClan.Controllers;
using ReciClan.Services;
using Microsoft.AspNetCore.Mvc;

namespace TestReciClan;

public class PreferenciasTests
{
    [Fact]
    public void Guardar_DeberiaPersistirYRetornarOk()
    {
        var dto = new PreferenciasDto(
            new[] { "reportes", "eventos" },
            "diaria",
            5.0
        );

        var mock = new Mock<IPreferenciasService>();
        mock.Setup(s => s.Guardar("user@test.com", dto));

        var controller = new PreferenciasController(mock.Object);

        var result = controller.Guardar("user@test.com", dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);

        mock.Verify(s => s.Guardar("user@test.com", dto), Times.Once);
    }
}