using SQSWebApiConsumer;
using SQSWebApiConsumer.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ReceiveMessage>();
builder.Services.AddHostedService<BackgroundServiceForSqs>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
