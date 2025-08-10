using System.Net;
using System.Net.Sockets;
using System.Text;

var udp = new UdpClient(9000);
Console.WriteLine("UDP server started on port 9000");

while (true)
{
    var result = await udp.ReceiveAsync();
    string message = Encoding.UTF8.GetString(result.Buffer);
    Console.WriteLine($"Received: {message} from {result.RemoteEndPoint}");

    string reply = "I have changed your position!";
    byte[] replyData = Encoding.UTF8.GetBytes(reply);
    await udp.SendAsync(replyData, replyData.Length, result.RemoteEndPoint);
}


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

//app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
