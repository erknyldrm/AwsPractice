using System;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSWebApiPublisher.Messaging;

public class SendMessage(IAmazonSQS sqsClient)
{
    public async Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var queueUrlResponse = await sqsClient.GetQueueUrlAsync("TestQueue", cancellationToken);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        var response = await sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);

        return response;
        
    }

}
