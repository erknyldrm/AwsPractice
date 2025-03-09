using System;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSWebApiConsumer.Messaging;

public sealed class ReceiveMessage
{
    public async Task ReceiveMessageAsync<T>(CancellationToken cancellationToken = default)
    {
        var sqlClient = new AmazonSQSClient();

        var queueUrlResponse = await sqlClient.GetQueueUrlAsync("TestQueue", cancellationToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageAttributeNames = ["All"],   
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            var response = await sqlClient.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);

            foreach (var item in response.Messages)
            {
                // send mail
                // save to db

                await Console.Out.WriteLineAsync($"Message Id:{item.MessageId}");

                await sqlClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, item.ReceiptHandle);

            }

            await Task.Delay(100, cancellationToken);
        }


    }
}
