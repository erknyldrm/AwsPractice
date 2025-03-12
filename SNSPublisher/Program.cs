using System.Reflection.Metadata;
using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

var message = new 
{
    Name = "Test"
};

var snsClient = new AmazonSimpleNotificationServiceClient();

var topicArnResponse = await snsClient.FindTopicAsync("TestNotification");

var publishRequest = new PublishRequest
{
    TopicArn = topicArnResponse.TopicArn,
    Message = JsonSerializer.Serialize(message),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>{
        {
            "MessageType",new MessageAttributeValue
            {
                DataType = "String",
                StringValue = "User"
            }
        }
    }
};

var response = snsClient.PublishAsync(publishRequest);



