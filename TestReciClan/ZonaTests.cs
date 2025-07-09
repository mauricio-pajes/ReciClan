using Microsoft.AspNetCore.Mvc;
using Moq;
using ReciClan.Controllers;
using ReciClan.Services;

namespace TestReciClan
{
    public class ZonaServiceMockTests
    {
        [Fact]
        public void Crear_DeberiaRetornarZonaConId()
        {
            var req = new ZonaReq(-12.05, -77.04, "Basura en la playa", []);

            var expected = new ZonaRes(1, DateTime.UtcNow, req.Descripcion, []);
            var serviceMoq = new Mock<IZonaService>();
            serviceMoq.Setup(s => s.Crear(req)).Returns(expected);

            var result = serviceMoq.Object.Crear(req);

            Assert.Equal(expected, result);
            serviceMoq.Verify(s => s.Crear(req), Times.Once);
        }

        [Fact]
        public void Crear_ConFotos_DeberiaMantenerFotos()
        {
            var fotos = new[] { "foto1.jpg", "foto2.jpg" };
            var req = new ZonaReq(0, 0, "Esquina sucia", fotos);

            var expected = new ZonaRes(33, DateTime.UtcNow, req.Descripcion, fotos);
            var serviceMoq = new Mock<IZonaService>();
            serviceMoq.Setup(s => s.Crear(req)).Returns(expected);

            var result = serviceMoq.Object.Crear(req);

            Assert.Equal(fotos, result.Fotos);
            serviceMoq.Verify(s => s.Crear(req), Times.Once);
        }
    }

    public class ZonaControllerMockTests
    {
        [Fact]
        public void Crear_DeberiaRetornarCreatedConId()
        {
            var req = new ZonaReq(0, 0, "algo", []);
            var expectedRes = new ZonaRes(99, DateTime.UtcNow, "ok", []);

            var serviceMoq = new Mock<IZonaService>();
            serviceMoq.Setup(z => z.Crear(It.IsAny<ZonaReq>()))
                .Returns(expectedRes);

            var controller = new ZonaController(serviceMoq.Object);

            var result = controller.Crear(req);

            var created = Assert.IsType<CreatedResult>(result);
            var body = Assert.IsType<ZonaRes>(created.Value);
            Assert.Equal(99, body.Id);

            serviceMoq.Verify(z => z.Crear(req), Times.Once);
        }
    }
}