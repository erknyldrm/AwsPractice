
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;


string accessKey = "";
string secretKey = "";
var region = Amazon.RegionEndpoint.EUNorth1;

//var sqsClient = new AmazonSQSClient(accessKey, secretKey, region); // another pc
var sqsClient = new AmazonSQSClient(region); 

var testQueue = new 
{
    Country = "Turkiye",
    City = "Ankara"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("TestQueue");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(testQueue),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = "TestQueue"
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.ReadLine();
