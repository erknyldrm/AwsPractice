using System;
using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SNSWebApiPublisher.Messaging;

public class SendMessage(IAmazonSimpleNotificationService snsClient)
{
    public async Task<PublishResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var topicArnResponse = await snsClient.FindTopicAsync("TestNotification");


        var publishRequest = new PublishRequest
        {
        TopicArn = topicArnResponse.TopicArn,
        Message = JsonSerializer.Serialize(message),
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

        var response = await snsClient.PublishAsync(publishRequest);

        return response;
        
    }

}