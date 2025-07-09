using Microsoft.AspNetCore.Mvc;
using Moq;
using ReciClan.Controllers;
using ReciClan.Services;

namespace TestReciClan;

public class EventoTests
{
    [Fact]
    public void Listar_DeberiaRetornarEventosConCamposBasicos()
    {
        var listaFake = new[]
        {
            new EventoLista(1,"Evento A",DateTime.Today,"Ubicacion A"),
            new EventoLista(2,"Evento B",DateTime.Today,"Ubicacion B")
        };
        var svc = new Mock<IEventoService>();
        svc.Setup(s => s.Listar()).Returns(listaFake);

        var controller = new EventoController(svc.Object);

        var result = controller.Listar();

        Assert.Equal(listaFake, result);
        Assert.All(result, e =>
        {
            Assert.NotNull(e.Nombre);
            Assert.NotNull(e.Ubicacion);
        });
        svc.Verify(s => s.Listar(), Times.Once);
    }

    [Fact]
    public void Detalle_DeberiaRetornarInfoCompleta()
    {
        var det = new EventoDetalle(5,"Evento X",DateTime.Now,
                                    "Lugar X","Resp","Recs");
        var svc = new Mock<IEventoService>();
        svc.Setup(s => s.Obtener(5)).Returns(det);

        var controller = new EventoController(svc.Object);
        var res = controller.Detalle(5);

        var ok = Assert.IsType<EventoDetalle>(res.Value);
        Assert.Equal(det, ok);
        svc.Verify(s => s.Obtener(5), Times.Once);
    }

    [Fact]
    public void Registrar_DeberiaProgramarNotificacion()
    {
        var notif = new Mock<INotificacionService>();
        var svc   = new Mock<IEventoService>();

        svc.Setup(s => s.Registrar(2, "user@test.com"))
           .Callback<int,string>((_,_) =>
                notif.Object.EnviarRecordatorio(2,"user@test.com",
                                                DateTime.Today));

        var controller = new EventoController(svc.Object);

        var res = controller.Registrar(2, "user@test.com");

        Assert.IsType<OkObjectResult>(res);
        notif.Verify(n =>
            n.EnviarRecordatorio(2, "user@test.com", It.IsAny<DateTime>()),
            Times.Once);
    }
}
