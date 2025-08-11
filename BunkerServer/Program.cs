var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<GameUdpService>();

var app = builder.Build();


app.Run();
