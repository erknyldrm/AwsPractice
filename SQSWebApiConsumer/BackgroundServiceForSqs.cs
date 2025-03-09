using System;
using SQSWebApiConsumer.Messaging;
using SQSWebApiConsumer.Models;

namespace SQSWebApiConsumer;

public class BackgroundServiceForSqs(ReceiveMessage receiveMessage) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await receiveMessage.ReceiveMessageAsync<List<Order>>(stoppingToken);
        
    }
}
