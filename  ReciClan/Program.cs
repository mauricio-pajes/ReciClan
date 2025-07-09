using ReciClan.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReciClanService, ReciClanService>();
builder.Services.AddScoped<IResumenService, ResumenService>();
builder.Services.AddScoped<IZonaService, ZonaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddSingleton<INotificacionService, DummyNotifService>();
builder.Services.AddScoped<IPreferenciasService, PreferenciasService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class DummyNotifService : INotificacionService
{
    public void EnviarRecordatorio(int _, string __, DateTime ___) { }
}
